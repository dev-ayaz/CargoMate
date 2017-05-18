var VehicleTypes = {
    selectors: {
        AddVehicleTypeForm: "#AddVehicleTypeForm",
        tblVehicletypeList: "#tblVehicletypeList",
        btnVehicletypeDelete: ".VehicletypeDelete",
        AddVehicleTypeFormContent: "#AddVehicleTypeFormContent",
        btnVehicletypeEdit: ".VehicletypeEdit",
        modalAddNewVehicleType: "#modalAddNewVehicleType",
        modalEditVehicleType: "#modalEditVehicleType",
        EditVehicleTypeFormContent: "#EditVehicleTypeFormContent",
        EditVehicleTypeForm: "#EditVehicleTypeForm"

        
    },
    services: {
        controller: "Vehicle",
        actions: {
            AddVehicleType: "AddVehicleType",
            VehicletypeList: "VehicletypeList",
            DeleteVehicleType: "DeleteVehicleType",
            EditVehicleType: "EditVehicleType"
        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(VehicleTypes.selectors.AddVehicleTypeForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.VehicletypeList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicleTypes.selectors.tblVehicletypeList).html(response);
                    
                    VehicleTypes.initEvents();
                });

 
            }
        },
        updateSuccess:function(result) {
              CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                var url = [RequestHandler.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.VehicletypeList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function(response) {

                    $(VehicleTypes.selectors.tblVehicletypeList).html(response);

                    VehicleTypes.initEvents();
                });

                $(VehicleTypes.selectors.modalEditVehicleType).modal("hide");
            }
        },
        deleteVehicleType: function ($this) {
            
            var typeId = $this.attr("data-typeid");

            var url = [RequestHandler.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.DeleteVehicleType].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { typeId: typeId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader,result.Message,result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleType: function ($this) {

            var typeId = $this.attr("data-typeid");

            var url = [RequestHandler.getSiteRoot(), VehicleTypes.services.controller, "/", VehicleTypes.services.actions.EditVehicleType].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { typeId: typeId }, function (result) {
                $(VehicleTypes.selectors.EditVehicleTypeFormContent).html(result);
                $(VehicleTypes.selectors.modalEditVehicleType).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

           
        }
    },
    initEvents: function () {

        $(VehicleTypes.selectors.btnVehicletypeDelete).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleTypes.callbacks.deleteVehicleType($this);
                }
               
            });
        });
        $(VehicleTypes.selectors.btnVehicletypeEdit).click(function () {
            var $this = $(this);
            VehicleTypes.callbacks.editVehicleType($this);
        });
        
        $(VehicleTypes.selectors.AddVehicleTypeForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicleTypes.selectors.AddVehicleTypeForm).attr("action");
            var formData = new FormData($(VehicleTypes.selectors.AddVehicleTypeForm).get(0));
            console.log(formData);
          
            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleTypes.callbacks.insertSuccess(data);
                }
            });
            return false;
        });

        $(VehicleTypes.selectors.EditVehicleTypeForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicleTypes.selectors.EditVehicleTypeForm).attr("action");
            var formData = new FormData($(VehicleTypes.selectors.EditVehicleTypeForm).get(0));


            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleTypes.callbacks.updateSuccess(data);
                }
            });
            return false;
        });
    }
};
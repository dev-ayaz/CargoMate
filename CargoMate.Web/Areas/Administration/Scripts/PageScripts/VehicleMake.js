var VehicleMake = {
    selectors: {
        AddVehicleMakeForm: "#AddVehicleMakeForm",
        tblVehicleMakesList: "#tblVehicleMakesList",
        btnVehicleMakeDelete: ".VehicleMakeDelete",
        btnVehicleMakeEdit: ".VehicleMakeEdit",
        EditVehicleMakeFormContent: "#EditVehicleMakeFormContent",
        EditVehicleMakeForm: "#UpdateVehicleMakeForm",
        modalEditVehicleMake: "#modalEditVehicleMake"
    },
    services: {
        controller: "Vehicle",
        actions: {
            MakeList: "MakeList",
            DeletMake: "DeletMake",
            EditVehicleMake: "EditVehicleMake"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(VehicleMake.selectors.AddVehicleMakeForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.MakeList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicleMake.selectors.tblVehicleMakesList).html(response);
                    VehicleMake.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                var url = [RequestHandler.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.MakeList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicleMake.selectors.tblVehicleMakesList).html(response);
                    $(VehicleMake.selectors.modalEditVehicleMake).modal("hide");
                    VehicleMake.initEvents();
                });
            }
        },
        deleteVehicleMake: function ($this) {

            var makeId = $this.attr("data-makeid");

            var url = [RequestHandler.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.DeletMake].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { makeId: makeId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        EditVehicleMake: function ($this) {

            var makeId = $this.attr("data-makeid");

            var url = [RequestHandler.getSiteRoot(), VehicleMake.services.controller, "/", VehicleMake.services.actions.EditVehicleMake].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { makeId: makeId }, function (result) {
                $(VehicleMake.selectors.EditVehicleMakeFormContent).html(result);
                $(VehicleMake.selectors.modalEditVehicleMake).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

            
        }
    },
    initEvents: function () {

        $(VehicleMake.selectors.btnVehicleMakeDelete).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleMake.callbacks.deleteVehicleMake($this);
                }

            });
        });

        $(VehicleMake.selectors.btnVehicleMakeEdit).click(function () {
            var $this = $(this);
            VehicleMake.callbacks.EditVehicleMake($this);
        });
        
        $(VehicleMake.selectors.AddVehicleMakeForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicleMake.selectors.AddVehicleMakeForm).attr("action");
            var formData = new FormData($(VehicleMake.selectors.AddVehicleMakeForm).get(0));
            console.log(formData);

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleMake.callbacks.insertSuccess(data);
                }
            });
            return false;
        });
        $(VehicleMake.selectors.EditVehicleMakeForm).submit(function (e) {
            debugger;
            e.preventDefault();
            var action = $(VehicleMake.selectors.EditVehicleMakeForm).attr("action");
            var formData = new FormData($(VehicleMake.selectors.EditVehicleMakeForm).get(0));

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleMake.callbacks.updateSuccess(data);
                }
            });
            return false;
        });

    }
};
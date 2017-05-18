var VehicleConfigurations = {
    selectors: {
        AddVehicleConfigurationForm: "#AddVehicleConfigurationForm",
        tblVehicleConfigurationList: "#tblVehicleConfigurationList",
        btnVehicleConfigurationDelete: ".VehicleConfigurationDelete",
        btnVehicleConfigurationEdit: ".VehicleConfigurationEdit",
        EditVehicleConfigurationFormContent: "#EditVehicleConfigurationFormContent",
        modalEditVehicleConfiguration: "#EditVehicleConfigurationModal",
        UpdateVehicleConfigurationForm: "#UpdateVehicleConfigurationForm"
       
    },
    services: {
        controller: "Vehicle",
        actions: {
            VehicleConfigurationList: "VehicleConfigurationList",
            DeleteVehicleConfiguration: "DeleteVehicleConfiguration",
            UpdateVehicleConfiguration: "UpdateVehicleConfiguration",
            EditVehicleConfiguration: "EditVehicleConfiguration"
        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(VehicleConfigurations.selectors.AddVehicleConfigurationForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), VehicleConfigurations.services.controller, "/", VehicleConfigurations.services.actions.VehicleConfigurationList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicleConfigurations.selectors.tblVehicleConfigurationList).html(response);
                    $(VehicleConfigurations.selectors.modalEditVehicleConfiguration).modal("hide");
                    VehicleConfigurations.initEvents();
                });
            }
        },
        deleteVehicleConfiguration: function ($this) {

            var configurationId = $this.attr("data-configurationId");

            var url = [RequestHandler.getSiteRoot(), VehicleConfigurations.services.controller, "/", VehicleConfigurations.services.actions.DeleteVehicleConfiguration].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { configurationId: configurationId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleConfiguration: function ($this) {

            var configurationId = $this.attr("data-configurationid");

            var url = [RequestHandler.getSiteRoot(), VehicleConfigurations.services.controller, "/", VehicleConfigurations.services.actions.EditVehicleConfiguration].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { configurationId: configurationId }, function (result) {
                $(VehicleConfigurations.selectors.EditVehicleConfigurationFormContent).html(result);
                $(VehicleConfigurations.selectors.modalEditVehicleConfiguration).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

           
        }
    },
    initEvents: function () {

        $(VehicleConfigurations.selectors.btnVehicleConfigurationDelete).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleConfigurations.callbacks.deleteVehicleConfiguration($this);
                }

            });
        });
        $(VehicleConfigurations.selectors.btnVehicleConfigurationEdit).click(function () {
            var $this = $(this);
            VehicleConfigurations.callbacks.editVehicleConfiguration($this);
        });
        
        $(VehicleConfigurations.selectors.AddVehicleConfigurationForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicleConfigurations.selectors.AddVehicleConfigurationForm).attr("action");
            var formData = new FormData($(VehicleConfigurations.selectors.AddVehicleConfigurationForm).get(0));
            console.log(formData);

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleConfigurations.callbacks.insertSuccess(data);
                }
            });
            return false;
        });

        $(VehicleConfigurations.selectors.UpdateVehicleConfigurationForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicleConfigurations.selectors.UpdateVehicleConfigurationForm).attr("action");
            var formData = new FormData($(VehicleConfigurations.selectors.UpdateVehicleConfigurationForm).get(0));
            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {
                    VehicleConfigurations.callbacks.insertSuccess(data);
                }
            });
            return false;
        });
    }
};
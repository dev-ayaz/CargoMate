var VehicleCapicities = {
    selectors: {
        AddVehicleCapicityForm: "#AddVehicleCapicityForm",
        tblVehicleCapicitiesList: "#tblVehicleCapicitiesList",
        btnVehicleCapicityDelete: ".VehicleCapicityDelete",
        EditCapicityForm: "#EditCapicityForm",
        btnVehicleCapicityEdit: ".VehicleCapicityEdit",
        EditVehicleCapicityFormContent: "#EditVehicleCapicityFormContent",
        EditVehicleCapicityForm: "#EditVehicleCapicityForm",
        modalEditVehicleCapicity: "#modalEditVehicleCapicity"
    },
    services: {
        controller: "Vehicle",
        actions: {
            VehicleCapicitiesList: "VehicleCapacitiesList",
            VehicleCapicityDelete: "VehicleCapacityDelete",
            EditVehicleCapicity: "EditVehicleCapacity"
          
        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(VehicleCapicities.selectors.AddVehicleCapicityForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.VehicleCapicitiesList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicleCapicities.selectors.tblVehicleCapicitiesList).html(response);
                    VehicleCapicities.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                var url = [RequestHandler.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.VehicleCapicitiesList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicleCapicities.selectors.tblVehicleCapicitiesList).html(response);
                    $(VehicleCapicities.selectors.modalEditVehicleCapicity).modal("hide");
                    VehicleCapicities.initEvents();
                });
            }
        },
        deleteVehicleCapicity: function ($this) {

            var capicityId = $this.attr("data-capicityid");

            var url = [RequestHandler.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.VehicleCapicityDelete].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { capacityId: capicityId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleCapicity: function ($this) {

            var capicityId = $this.attr("data-capicityid");

            var url = [RequestHandler.getSiteRoot(), VehicleCapicities.services.controller, "/", VehicleCapicities.services.actions.EditVehicleCapicity].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { capacityId: capicityId }, function (result) {
                $(VehicleCapicities.selectors.EditVehicleCapicityFormContent).html(result);
                $(VehicleCapicities.selectors.modalEditVehicleCapicity).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });
           
        }
    },
    initEvents: function () {

        $(VehicleCapicities.selectors.btnVehicleCapicityDelete).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicleCapicities.callbacks.deleteVehicleCapicity($this);
                }

            });
        });

        $(VehicleCapicities.selectors.btnVehicleCapicityEdit).click(function () {
            var $this = $(this);
            VehicleCapicities.callbacks.editVehicleCapicity($this);
        });
        
    }
};
var Weight = {
    selectors: {
        AddWeightForm: "#AddWeightForm",
        tblWeightList: "#tblWeightList",
        btnWeightDelete: ".WeightDelete",
        EditCapicityForm: "#EditCapicityForm",
        btnVehicleCapicityEdit: ".VehicleCapicityEdit",
        EditVehicleCapicityFormContent: "#EditVehicleCapicityFormContent",
        EditVehicleCapicityForm: "#EditVehicleCapicityForm",
        modalEditVehicleCapicity: "#modalEditVehicleCapicity"
    },
    services: {
        controller: "Vehicle",
        actions: {
            WeightList: "WeightList",
            DeleteWeight: "DeleteWeight",
            EditVehicleCapicity: "EditVehicleCapacity"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(Weight.selectors.AddWeightForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.WeightList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(Weight.selectors.tblWeightList).html(response);
                    Weight.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                var url = [RequestHandler.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.WeightList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(Weight.selectors.tblWeightList).html(response);
                    $(Weight.selectors.modalEditVehicleCapicity).modal("hide");
                    Weight.initEvents();
                });
            }
        },
        deleteWeight: function ($this) {

            var weightId = $this.attr("data-id");

            var url = [RequestHandler.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.DeleteWeight].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { weightId: weightId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        editVehicleCapicity: function ($this) {

            var capicityId = $this.attr("data-capicityid");

            var url = [RequestHandler.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.EditVehicleCapicity].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { capacityId: capicityId }, function (result) {
                $(Weight.selectors.EditVehicleCapicityFormContent).html(result);
                $(Weight.selectors.modalEditVehicleCapicity).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

        }
    },
    initEvents: function () {

        $(Weight.selectors.btnWeightDelete).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    Weight.callbacks.deleteWeight($this);
                }

            });
        });

        $(Weight.selectors.btnVehicleCapicityEdit).click(function () {
            var $this = $(this);
            Weight.callbacks.editVehicleCapicity($this);
        });

    }
};
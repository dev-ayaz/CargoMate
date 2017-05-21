var Weight = {
    selectors: {
        AddWeightForm: "#AddWeightForm",
        tblWeightList: "#tblWeightList",
        btnWeightDelete: ".WeightDelete",
        EditCapicityForm: "#EditCapicityForm",
        btnWeightEdit: ".WeightEdit",
        EditWeightFormContent: "#EditWeightFormContent",
        EditWeightForm: "#EditWeightForm",
        modalEditWeight: "#modalEditWeight"
    },
    services: {
        controller: "Vehicle",
        actions: {
            WeightList: "WeightList",
            DeleteWeight: "DeleteWeight",
            EditWeight: "EditWeight"

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
                    $(Weight.selectors.modalEditWeight).modal("hide");
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
        EditWeight: function ($this) {

            var weightId = $this.attr("data-id");

            var url = [RequestHandler.getSiteRoot(), Weight.services.controller, "/", Weight.services.actions.EditWeight].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { weightId: weightId }, function (result) {
                $(Weight.selectors.EditWeightFormContent).html(result);
                $(Weight.selectors.modalEditWeight).modal("toggle");
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

        $(Weight.selectors.btnWeightEdit).click(function () {
            var $this = $(this);
            Weight.callbacks.EditWeight($this);
        });

    }
};
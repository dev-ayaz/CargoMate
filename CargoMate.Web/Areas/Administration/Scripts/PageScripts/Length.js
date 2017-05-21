var Length = {
    selectors: {
        AddLengthForm: "#AddLengthForm",
        tblLengthList: "#tblLengthList",
        btnLengthDelete: ".LengthDelete",
        btnLengthEdit: ".LengthEdit",
        EditLengthFormContent: "#EditLengthFormContent",
        EditLengthForm: "#UpdateLengthForm",
        modalEditLength: "#EditLengthModal"
    },
    services: {
        controller: "Vehicle",
        actions: {
            LengthList: "LengthList",
            DeleteLength: "DeleteLength",
            EditLength: "EditLength"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(Length.selectors.AddLengthForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), Length.services.controller, "/", Length.services.actions.LengthList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(Length.selectors.tblLengthList).html(response);
                    Length.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                var url = [RequestHandler.getSiteRoot(), Length.services.controller, "/", Length.services.actions.LengthList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(Length.selectors.tblLengthList).html(response);
                    $(Length.selectors.modalEditLength).modal("hide");
                    Length.initEvents();
                });
            }
        },
        deleteLength: function ($this) {

            var lengthId = $this.attr("data-id");

            var url = [RequestHandler.getSiteRoot(), Length.services.controller, "/", Length.services.actions.DeleteLength].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { lengthId: lengthId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });
        },
        EditLength: function ($this) {

            var LengthId = $this.attr("data-id");

            var url = [RequestHandler.getSiteRoot(), Length.services.controller, "/", Length.services.actions.EditLength].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { LengthId: LengthId }, function (result) {
                $(Length.selectors.EditLengthFormContent).html(result);
                $(Length.selectors.modalEditLength).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });

        }
    },
    initEvents: function () {

        $(Length.selectors.btnLengthDelete).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    Length.callbacks.deleteLength($this);
                }

            });
        });

        $(Length.selectors.btnLengthEdit).click(function () {
            var $this = $(this);
            Length.callbacks.EditLength($this);
        });

    }
};
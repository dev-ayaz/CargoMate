var PayLoadTypes = {
    selectors: {
        AddPayLoadtypeForm: "#AddPayLoadtypeForm",
        tblPayLoadTypeList: "#tblPayLoadTypeList",
        btnDeletePayLoadTypes: ".PayLoadDelete",
        btnPayLoadTypeEdit: ".PayLoadEdit",
        UpdatePayLoadTypeForm: "#UpdatePayLoadTypeForm",
        EditPayLoadTypeContent: "#EditPayLoadTypeContent",
        modalEditPayLoadType: "#modalEditPayLoadType"
    },
    services: {
        controller: "Vehicle",
        actions: {
            PayLoadTypeList: "PayLoadTypeList",
            DeletePayLoadTypes: "DeletePayLoadTypes",
            PayLoadEdit: "PayLoadEdit"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(PayLoadTypes.selectors.AddPayLoadtypeForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.PayLoadTypeList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(PayLoadTypes.selectors.tblPayLoadTypeList).html(response);
                    PayLoadTypes.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                var url = [RequestHandler.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.PayLoadTypeList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(PayLoadTypes.selectors.tblPayLoadTypeList).html(response);
                    PayLoadTypes.initEvents();
                    $(PayLoadTypes.selectors.modalEditPayLoadType).modal("toggle");
                });
            }
            },
        deletePayloadType: function ($this) {

            var payloadId = $this.attr("data-payloadid");

            var url = [RequestHandler.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.DeletePayLoadTypes].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { payloadId: payloadId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });


        },
        EditPayLoadType: function ($this) {

            var payloadId = $this.attr("data-payloadid");

            var url = [RequestHandler.getSiteRoot(), PayLoadTypes.services.controller, "/", PayLoadTypes.services.actions.PayLoadEdit].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { payloadId: payloadId }, function (result) {
                $(PayLoadTypes.selectors.EditPayLoadTypeContent).html(result);
                $(PayLoadTypes.selectors.modalEditPayLoadType).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });
        }
    },
    initEvents: function () {


        $(PayLoadTypes.selectors.btnDeletePayLoadTypes).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    PayLoadTypes.callbacks.deletePayloadType($this);
                }

            });
        });

        $(PayLoadTypes.selectors.btnPayLoadTypeEdit).click(function () {
            var $this = $(this);
            PayLoadTypes.callbacks.EditPayLoadType($this);
        });

        $(PayLoadTypes.selectors.AddPayLoadtypeForm).submit(function (e) {

            e.preventDefault();
            var action = $(PayLoadTypes.selectors.AddPayLoadtypeForm).attr("action");
            var formData = new FormData($(PayLoadTypes.selectors.AddPayLoadtypeForm).get(0));

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {

                    PayLoadTypes.callbacks.insertSuccess(data);
                }
            });
            return false;
        });

        $(PayLoadTypes.selectors.UpdatePayLoadTypeForm).submit(function (e) {

            e.preventDefault();
            var action = $(PayLoadTypes.selectors.UpdatePayLoadTypeForm).attr("action");
            var formData = new FormData($(PayLoadTypes.selectors.UpdatePayLoadTypeForm).get(0));

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {

                    PayLoadTypes.callbacks.updateSuccess(data);
                }
            });
            return false;
        });

    }
};
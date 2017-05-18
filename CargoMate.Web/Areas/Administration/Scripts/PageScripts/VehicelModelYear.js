var VehicelModelYear = {
    selectors: {
        AddVehicelModelYearForm: "#AddVehicelModelYearForm",
        tblModelYearList: "#tblModelYearList",
        btnDeleteVehicelModelYear: ".DeleteVehicelModelYear",
        btnVehicleModelEdit: ".VehicleModelEdit",
        EditVehicleModelYearFormContent: "#EditVehicleModelYearFormContent",
        EditVehicleModelYearForm: "#EditVehicleModelYearForm",
        modalEditVehicleModelYear: "#modalEditVehicleModelYear"
    },
    services: {
        controller: "Vehicle",
        actions: {
            VehicleModelYearList: "VehicleModelYearList",
            DeleteVehicelModelYear: "DeleteVehicelModelYear",
            EditVehicleModelYear: "EditVehicleModelYear"

        }
    },
    callbacks: {
        insertSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                $(VehicelModelYear.selectors.AddVehicelModelYearForm)[0].reset();
                var url = [RequestHandler.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.VehicleModelYearList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicelModelYear.selectors.tblModelYearList).html(response);
                    VehicelModelYear.initEvents();
                });
            }
        },
        updateSuccess: function (result) {
            CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
            if (!result.IsError) {
                var url = [RequestHandler.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.VehicleModelYearList].join("");

                RequestHandler.postToController(url, RequestHandler.formMethods.Get, {}, function (response) {

                    $(VehicelModelYear.selectors.tblModelYearList).html(response);
                    $(VehicelModelYear.selectors.modalEditVehicleModelYear).modal("hide");
                    VehicelModelYear.initEvents();
                });
            }
        },
        deleteVehicleModelYear: function ($this) {

            var modelYearId = $this.attr("data-modelyearid");

            var url = [RequestHandler.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.DeleteVehicelModelYear].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { modelYearId: modelYearId }, function (result) {
                CargoMateAlerts.actionAlert(result.MessageHeader, result.Message, result.IsError);
                if (!result.IsError) {
                    $this.closest("tr").remove();
                }
            });

            
        },
        EditVehicleModelYear: function ($this) {

            var modelYearId = $this.attr("data-modelyearid");

            var url = [RequestHandler.getSiteRoot(), VehicelModelYear.services.controller, "/", VehicelModelYear.services.actions.EditVehicleModelYear].join("");
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { modelYearId: modelYearId }, function (result) {
                $(VehicelModelYear.selectors.EditVehicleModelYearFormContent).html(result);
                $(VehicelModelYear.selectors.modalEditVehicleModelYear).modal("toggle");
                $("select").select2({
                    dropdownAutoWidth: true,
                    width: false
                });
            });
        }
    },
    initEvents: function () {


        $(VehicelModelYear.selectors.btnDeleteVehicelModelYear).click(function () {
            var $this = $(this);
            CargoMateAlerts.confirm(function (isConfirm) {
                if (isConfirm) {
                    VehicelModelYear.callbacks.deleteVehicleModelYear($this);
                }

            });
        });

        $(VehicelModelYear.selectors.btnVehicleModelEdit).click(function () {
            var $this = $(this);
            VehicelModelYear.callbacks.EditVehicleModelYear($this);
        });

        $(VehicelModelYear.selectors.AddVehicelModelYearForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicelModelYear.selectors.AddVehicelModelYearForm).attr("action");
            var formData = new FormData($(VehicelModelYear.selectors.AddVehicelModelYearForm).get(0));

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {

                    VehicelModelYear.callbacks.insertSuccess(data);
                }
            });
            return false;
        });

        $(VehicelModelYear.selectors.EditVehicleModelYearForm).submit(function (e) {

            e.preventDefault();
            var action = $(VehicelModelYear.selectors.EditVehicleModelYearForm).attr("action");
            var formData = new FormData($(VehicelModelYear.selectors.EditVehicleModelYearForm).get(0));

            $.ajax({
                type: "POST",
                url: action,
                data: formData,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function (data) {

                    VehicelModelYear.callbacks.updateSuccess(data);
                }
            });
            return false;
        });
        
    }
};
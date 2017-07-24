var Customers = {
    Selectors: {

        IsCompanyDropDown: "#IsCompany",
        CompanyItemDiv: "#div-Company",
        AddressPicker: "#Address",
        Phone: "#PhoneNumber",
        Location: "#Location",
        Imageinput: "#Imageinput",
        UserImage: "#ImageUrl",
        CustomerId: "#CustomerId",
        Name: "#Name",
        EmailAddress: "#EmailAddress"
        
    },
    Services: {

        Controller: "Customer",
        Actions: {
            CheckCustoer: "IsCustomerExists"
        }
    },
    CallBacks: {
        IsCustomerExists: function (user) {

            sessionStorage.setItem("User", JSON.stringify(user));

            var url = [RequestHandler.getSiteBase(), "/", Customers.Services.Controller, "/", Customers.Services.Actions.CheckCustoer].join("");

            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { userId: user.uid }, function (response) {

                if (!response.IsExists) {

                    window.location.href = response.RedirectUrl;
                } else {
                    window.location.href = RequestHandler.getSiteBase();
                }
            });
        },
        InsertSuccess: function (response) {
            CargoMateAlerts.actionAlert(response.MessageHeader, response.Message, response.IsError);

            if (!response.IsError) {
                window.location.href = RequestHandler.getSiteBase();
            }
        }
    },
    InitEvents: function () {


        var user = JSON.parse(sessionStorage.getItem("User"));

        if (user != null) {
            if (user.displayName) {
                $(Customers.Selectors.Name).val(user.displayName);
                $(Customers.Selectors.Name).attr("disabled", "disabled");
            }
            if (user.email) {
                $(Customers.Selectors.EmailAddress).val(user.email);
                $(Customers.Selectors.EmailAddress).attr("disabled", "disabled");
            }

            if (user.phoneNumber) {
                $(Customers.Selectors.Phone).val(user.phoneNumber);
                $(Customers.Selectors.Phone).attr("disabled", "disabled");
            }

        }


        $(Customers.Selectors.CustomerId).val(RequestHandler.getQueryString("UId"));

        $(Customers.Selectors.Imageinput + ":not(.bound)").addClass("bound").change(function () {

            var imageInput = $(this);

            if (this.files && this.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $(imageInput.closest("form").find(Customers.Selectors.UserImage)).val(e.target.result);
                }

                reader.readAsDataURL(this.files[0]);
            }
        });

        $(Customers.Selectors.IsCompanyDropDown).change(function () {

            if ($(this).val() === "true") {
                $(Customers.Selectors.CompanyItemDiv).attr("hidden", false);
                return;
            }
            $(Customers.Selectors.CompanyItemDiv).attr("hidden", true);
        });

        $(Customers.Selectors.AddressPicker).placepicker({
            placeChanged: function (place) {

                var location = this.getLocation();
                $(Customers.Selectors.Location).val(location.latitude + "," + location.longitude);
            }
        }).data('placepicker');

        $(Customers.Selectors.Phone).intlTelInput({

            allowDropdown: true,
            autoHideDialCode: false,
            autoPlaceholder: "polite",
            customPlaceholder: null,
            dropdownContainer: "",
            excludeCountries: [],
            formatOnDisplay: true,
            geoIpLookup: function (callback) {
                $.get("http://ipinfo.io", function () { }, "jsonp").always(function (resp) {
                    var countryCode = (resp && resp.country) ? resp.country : "";
                    callback(countryCode);
                });
            },

            initialCountry: "",
            nationalMode: false,
            placeholderNumberType: "MOBILE",
            onlyCountries: [],
            preferredCountries: ["sa", "ae", "pk"],
            separateDialCode: false,
            utilsScript: ""
        });
    }
}
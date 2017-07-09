var Customers = {
    Selectors: {

        IsCompanyDropDown: "#IsCompany",
        CompanyItemDiv: "#div-Company",
        AddressPicker: "#Address",
        Phone: "#PhoneNumber"

    },
    Services: {

        Controller: "Customer",
        Actions: {
            IsCustomerExists: "IsCustomerExists"
        }
    },
    CallBacks: {
        IsCustomerExists: function (user) {
            debugger;
            console.log(user);
            var url = [RequestHandler.getSiteBase(), "/", Customers.Services.Controller, "/", Customers.Services.Actions.IsCustomerExists].join("");

            console.log(url);
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, { userId: user.uid }, function (response) {
                if (!response.IsExists) {
                    window.location.href = response.RedirectUrl;
                }
            });
        }
    },
    InitEvents: function () {
        $(Customers.Selectors.IsCompanyDropDown).change(function () {

            if ($(this).val() === "true") {
                $(Customers.Selectors.CompanyItemDiv).attr("hidden", false);
                return;
            }
            $(Customers.Selectors.CompanyItemDiv).attr("hidden", true);
        });

        $(Customers.Selectors.AddressPicker).placepicker({
            placeChanged: function (place) {
                console.log(place);
                var latlng = this.getLocation();
                var lat = latlng["latitude"];
                var long = latlng["longitude"];
            }
        }).data('placepicker');

        $(Customers.Selectors.Phone).intlTelInput({
            // whether or not to allow the dropdown
            allowDropdown: true,

            // if there is just a dial code in the input: remove it on blur, and re-add it on focus
            autoHideDialCode: true,

            // add a placeholder in the input with an example number for the selected country
            autoPlaceholder: "polite",

            // modify the auto placeholder
            customPlaceholder: null,

            // append menu to a specific element
            dropdownContainer: "",

            // don't display these countries
            excludeCountries: [],

            // format the input value during initialisation
            formatOnDisplay: true,

            // geoIp lookup function
            geoIpLookup: function(callback) {
                   $.get("http://ipinfo.io", function() {}, "jsonp").always(function(resp) {
                     var countryCode = (resp && resp.country) ? resp.country : "";
                     callback(countryCode);
                   });
                 },

            // initial country
            initialCountry: "",

            // don't insert international dial codes
            nationalMode: false,

            // number type to use for placeholders
            placeholderNumberType: "MOBILE",

            // display only these countries
            onlyCountries: [],

            // the countries at the top of the list. defaults to united states and united kingdom
            preferredCountries: ["sa", "ae","pk"],

            // display the country dial code next to the selected flag so it's not part of the typed number
            separateDialCode: false,

            // specify the path to the libphonenumber script to enable validation/formatting
            utilsScript: ""
        });
    }
}
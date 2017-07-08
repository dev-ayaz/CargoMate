var Customers = {
    Selectors: {

        IsCompanyDropDown: "#IsCompany",
        CompanyItemDiv: "#div-Company"

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
            var url = [RequestHandler.getSiteBase(),"/", Customers.Services.Controller, "/", Customers.Services.Actions.IsCustomerExists].join("");

            console.log(url);
            RequestHandler.postToController(url, RequestHandler.formMethods.Get, {userId:user.uid}, function (response) {
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
    }
}
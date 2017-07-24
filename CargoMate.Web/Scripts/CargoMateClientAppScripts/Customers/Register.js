var CustomerRegistration = {

    selectors: {
        signUpWithFacebookButton: "#signUpWithFacebook",
        signUpWithGoogleButton: "#signUpWithGoogle",
        CustomerRegistrationForm: "#CustomerRegistrationForm",
        EmailAddress: "#EmailAddress",
        Password: "#Password"
    },
    services: {
        controller: "Customer",
        actions: {
            Register: "Register"
        }
    },
    callbacks: {
        registerCustomer: function (user) {
  
            var url = [RequestHandler.getSiteBase(), "/", CustomerRegistration.services.controller, "/", CustomerRegistration.services.actions.Register].join("");

            var userModel = { CustomerId: user.uid, Name: user.displayName || "", EmailAddress: user.email, PhoneNumber : user.phoneNumber || "000" };


            $.ajax({
                type: "POST",
                url: url,
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(userModel),
                success: function(data) {
                    CargoMateAlerts.actionAlert(data.MessageHeader, data.Message, data.IsError);
                },
                error: function (data) { console.log(data) }
            });
           
        }

    },
    initevents: function () {
        $(CustomerRegistration.selectors.signUpWithFacebookButton + ":not(.bound)").addClass("bound").click(function () {
            var user = firebaseUtilFunc.signiInWithFacebook();
            console.log(user);
        });
        $(CustomerRegistration.selectors.signUpWithGoogleButton + ":not(.bound)").addClass("bound").click(function () {
            firebaseUtilFunc.signInwithGoogle().then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                } else {
                    CustomerRegistration.callbacks.registerCustomer(response.result);
                }
            });
        });
        $(CustomerRegistration.selectors.CustomerRegistrationForm).submit(function (e) {

            e.preventDefault();
            firebaseUtilFunc.createUserWithEmailAndPassword($(CustomerRegistration.selectors.EmailAddress).val(), $(CustomerRegistration.selectors.Password).val()).then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                }
                else {
                    CustomerRegistration.callbacks.registerCustomer(response.result);
                }
            });
            return false;
        });
    }
}
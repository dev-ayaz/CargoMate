var CustomerRegistration = {

    selectors: {
        signUpWithFacebookButton: "#signUpWithFacebook",
        signUpWithGoogleButton: "#signUpWithGoogle",
        CustomerRegistrationForm: "#CustomerRegistrationForm",
        EmailAddress: "#EmailAddress",
        Password: "#Password"
    },
    services: {
        controller: "",
        actions: {

        }
    },
    callbacks: {

    },
    initevents: function () {
        $(CustomerRegistration.selectors.signUpWithFacebookButton + ":not(.bound)").addClass("bound").click(function () {
            var user = firebaseUtilFunc.signiInWithFacebook();
            console.log(user);
        });
        $(CustomerRegistration.selectors.signUpWithGoogleButton + ":not(.bound)").addClass("bound").click(function () {
            firebaseUtilFunc.signInwithGoogle().then(function (result) {
                debugger;
                if (result.IsError) {
                    CargoMateAlerts.actionAlert("Error", result.result, true);
                }
                console.log(result);
            });
        });
        $(CustomerRegistration.selectors.CustomerRegistrationForm).submit(function (e) {

            e.preventDefault();
            firebaseUtilFunc.createUserWithEmailAndPassword($(CustomerRegistration.selectors.EmailAddress).val(), $(CustomerRegistration.selectors.Password).val()).then(function (response) {
                if (response.IsError) {
                    CargoMateAlerts.actionAlert("Error", response.result, true);
                }
                console.log(response.result);
            });
            return false;
        });
    }
}
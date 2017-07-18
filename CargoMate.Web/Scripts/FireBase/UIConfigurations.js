var uiConfig = {
    signInSuccessUrl: '#',
    signInOptions: [
     // Specify providers you want to offer your users.
     firebase.auth.GoogleAuthProvider.PROVIDER_ID,
     firebase.auth.FacebookAuthProvider.PROVIDER_ID,
     firebase.auth.TwitterAuthProvider.PROVIDER_ID
     //firebase.auth.EmailAuthProvider.PROVIDER_ID
    ],
    // Terms of service url can be specified and will show up in the widget.
    tosUrl: '#',
    'callbacks': {
        'signInSuccess': function (currentUser, credential, redirectUrl) {

            Customers.CallBacks.IsCustomerExists(currentUser);
            return false;
        }
    }
};
// Initialize the FirebaseUI Widget using Firebase.
var ui = new firebaseui.auth.AuthUI(firebase.auth());
// The start method will wait until the DOM is loaded.
ui.start('#socialAuthDiv', uiConfig);

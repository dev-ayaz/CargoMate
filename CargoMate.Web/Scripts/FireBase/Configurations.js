
var config = {
    apiKey: "AIzaSyAAgsBhSzZ3WgVmEbTOMJG4dDpZRKZS12M",
    authDomain: "prologix-5cc3c.firebaseapp.com",
    databaseURL: "https://prologix-5cc3c.firebaseio.com",
    storageBucket: "prologix-5cc3c.appspot.com",
    messagingSenderId: "729428235723"
};
firebase.initializeApp(config);




var firebaseUtilFunc = {

    logout: function () {
        window.firebase.auth().signOut().then(function (user) {
            console.log(user);
        }, function (error) {
            console.log(error);
        });
    },
    signInWithEmailAndPassword: function (email, password) {
        window.firebase.auth().signInWithEmailAndPassword(email, password).catch(function (error) {

            console.log(error);
            // Handle Errors here.
            var errorCode = error.code;
            var errorMessage = error.message;
            // ...
        });
    },
    createUserWithEmailAndPassword: function (email, password) {
        return window.firebase.auth().createUserWithEmailAndPassword(email, password).then(function (response) {
            return { result: response, IsError: false };
       }).catch(function (error) {
            return { result: error.message, IsError: true };
        });
    },
    signiInWithFacebook: function () {

        var facebbokProvider = new window.firebase.auth.FacebookAuthProvider();
        facebbokProvider.addScope('user_birthday');
        facebbokProvider.setCustomParameters({
            'display': 'popup'
        });

        window.firebase.auth().signInWithPopup(facebbokProvider).then(function (result) {

            return result.user;

        }).catch(function (error) {
            return error.message;
        });
    },

    signInwithGoogle: function () {

        var googleProvider = new window.firebase.auth.GoogleAuthProvider();

        googleProvider.setCustomParameters({
            "login_hint": "user@example.com",
            'display': 'popup'
        });

        window.firebase.auth().signInWithPopup(googleProvider).then(function (result) {
            // This gives you a Google Access Token. You can use it to access the Google API.
            var token = result.credential.accessToken;
            // The signed-in user info.
            var user = result.user;
            console.log(result.user);
            // ...
        }).catch(function (error) {
            // Handle Errors here.
            var errorCode = error.code;
            var errorMessage = error.message;
            // The email of the user's account used.
            var email = error.email;
            // The firebase.auth.AuthCredential type that was used.
            var credential = error.credential;
            // ...
        });
    }
}
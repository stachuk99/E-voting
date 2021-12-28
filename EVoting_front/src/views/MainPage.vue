<template>
    <div class="content">
        <Button @click="signInWithGoogle()"> Sign in with Google </Button>
    </div>
</template>
<script>
    import FetchHelper from "@/helpers/fetchHelper";
    import { setAuthToken, setLoggedInEmail } from "@/services/sessionProps";
    export default {
        name: "MainPage",
        methods: {
            async signInWithGoogle() {
                
                try {
                    const fetchHelper = new FetchHelper();
                    const googleUser = await this.$gAuth.signIn();
                    var idToken = googleUser.getAuthResponse().id_token;
                    var PublicKey = "publicsample";
                    const response = await fetch(
                        "https://localhost:5001/api/auth/google-request",
                        {
                            method: "POST",
                            headers: {
                                "Content-Type": "application/json",
                            },
                            body: JSON.stringify({
                                idToken,
                                PublicKey
                            }),
                        }
                    )
                        .then(fetchHelper.handleErrors)
                        .then((res) => res.json());
                    localStorage.setItem("bearer", response.accessToken);
                    setAuthToken(response.access_token);
                    console.log(googleUser);
                    setLoggedInEmail(googleUser.Email);

                    await this.$router.push({
                        name: "HomePage",
                    });
                    console.log(googleUser);
                } catch (e) {
                    setAuthToken(null);
                    setLoggedInEmail(null);
                }
            },
        },
    };
</script>
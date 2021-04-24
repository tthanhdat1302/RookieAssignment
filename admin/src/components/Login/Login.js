import React from 'react'
export default function Login(props) {
    // var createSession=function()
    // {
    //     return "SessionValueMakeItABitLongerasdasdasfasdlkaskdqwkl";
    // }
    // var createNonce=function()
    // {
    //     return "NonceValueasdwqrioalkskalcklsadlkwqdklaslkdkasldlsa";
    // }
    // var redirectUri=`${process.env.REACT_APP_ADMIN_URL}/signin-oidc`;
    // var responseType="id_token token";
    // var scope="openid rookie.api";
    // var authUrl="/connect/authorize/callback?client_id=react"
    // +"&redirect_uri="+encodeURIComponent(redirectUri)
    // +"&response_type="+encodeURIComponent(responseType)
    // +"&scope="+encodeURIComponent(scope)
    // +"&nonce="+createNonce()
    // +"&state="+createSession()
    
    // var returnUrl=encodeURIComponent(authUrl);
    return (
        // window.location.href="https://aoishin.azurewebsites.net/Identity/Account/Login?ReturnUrl="+returnUrl
        <div>
            {
                props.userManager.signinRedirect()
            }
        </div>
       
        
    );
};
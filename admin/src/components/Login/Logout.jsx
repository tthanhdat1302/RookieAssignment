import React from "react";
import { Authenticate } from "react-oidc-client";
import { useAsync } from "react-async";

export default function Logout() {
  return (
    <Authenticate
    //   loginCompletePath="/signin-oidc"
      logoutPath="/signout-callback-oidc"
      userManagerSettings={{
        loadUserInfo: true,
        authority: "https://localhost:5001",
        client_id: "react",
        // redirect_uri: "http://localhost:3000/signin-oidc",
        post_logout_redirect_uri: "http://localhost:3000/"+"Cookies"+"oidc",
      }}
    >
    </Authenticate>
  );
}

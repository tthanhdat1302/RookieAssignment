import React from "react";
import { Authenticate } from "react-oidc-client";
import { useAsync } from "react-async";
import {useHistory} from 'react-router-dom'

export default function Login() {
    const history=useHistory()
  return (
    <Authenticate
      loginCompletePath="/signin-oidc"
    //   logoutPath="/signout-callback-oidc"
      userManagerSettings={{
        loadUserInfo: true,
        authority: "https://localhost:5001",
        client_id: "react",
        redirect_uri: "http://localhost:3000/signin-oidc",
        // post_logout_redirect_uri: "http://localhost:3000/signout-callback-oidc",
      }}
    >
    </Authenticate>
    
  );
}

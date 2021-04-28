import React from 'react'
import Oidc from 'oidc-client'
export default function LogoutCallback() {
  
    const userManager=new Oidc.UserManager({userStore:new Oidc.WebStorageStateStore({store:window.localStorage}),});
    return (
        // extractTokens(window.location.href)
        userManager.signoutCallback().then(res=>{        
           window.location.href=process.env.REACT_APP_API_URL
        })
    )
}

import React from 'react'
export default function Logout(props) {

    return (
        <div>
            {
                localStorage.removeItem("role"),
                props.userManager.signoutRedirect()
            }
        </div>
       
        
    );
};
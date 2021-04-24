import React from 'react'
export default function Logout(props) {

    return (
        <div>
            {
                props.userManager.signoutRedirect()
            }
        </div>
       
        
    );
};
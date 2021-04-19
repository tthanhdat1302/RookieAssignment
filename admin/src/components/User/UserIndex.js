// https://localhost:5001/api/user

import Axios from 'axios';
import React,{useState,useEffect} from 'react'
import { useHistory } from 'react-router-dom'
import { Table,Button } from 'reactstrap';

export default function UserIndex() {
    const [user,setUser]=useState([])

    useEffect(()=>{
        Axios.get("https://localhost:5001/api/user").then(res=>{
            setUser(res.data)
        })
    },[])

   return(
    <div>
    <h3>All User</h3>
    <Table bordered>
        <thead>
            <tr>
                <th>User Id</th>
                <th>User Email</th>
                <th>User Password</th>
            </tr>
        </thead>
        <tbody>
            {
                user.map(user=>
                    <tr>
                        <td>{user.id}</td>
                        <td>{user.name}</td> 
                        <td>{user.password}</td>
                    </tr>    
                )
            }
        </tbody>
    </Table>
</div>
    )
}


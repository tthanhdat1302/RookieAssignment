import React,{useState,useEffect} from 'react'
import { Button, Form } from 'reactstrap';
import Axios from 'axios'
import { BrowserRouter as Router,useHistory} from 'react-router-dom'

export default function CreateCategory() {
    const [cate,setCate]=useState({
        Name:''
    })
    const history=useHistory()
    const handleFormSubmit=(e)=>{
        e.preventDefault()
        const formData=new FormData()
        formData.append('Name',e.Name)
        Axios.post('https://localhost:5001/api/category',cate).then(()=>history.push('/category'))
    }
    const handleChange=(e)=>{
        const {name,value}=e.target;
        setCate({...cate,[name]:value})
    }
    return (
        <div style={{padding:'0 150px 0 150px'}}>
        <h3 style={{margin:'10px 0 10px 0'}}>Add New Category</h3>
        <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
            <div className="form-group">
                <label>Category Name: </label>
                <input type="text" className="form-control" name="Name" onChange={handleChange}/>
            </div>             
            <div className="form-group">
                <Button color="success" type="submit" style={{marginRight:'20px'}}>Create</Button>
                <Button color="success" onClick={()=>history.push('/product')}>Cancel</Button>
            </div>
        </form>
    </div>
    )
}

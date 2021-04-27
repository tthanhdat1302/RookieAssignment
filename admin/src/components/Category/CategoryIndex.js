import Axios from 'axios';
import React,{useState,useEffect} from 'react'
import { useHistory } from 'react-router-dom'
import { Table,Button } from 'reactstrap';

export default function CategoryIndex() {
   
    const history=useHistory();
    const [category,setCategory]=useState([])
    useEffect(()=>{
        Axios.get(`${process.env.REACT_APP_API_URL}/api/category`).then(res=>{
            setCategory(res.data)
        })
    },[])

    const deleteCate=(id)=>{
        Axios.delete(`${process.env.REACT_APP_API_URL}/api/category/`+id).then(()=>{
            setCategory(category.filter(x=>x.id!=id))
        })
    }
    
    const createCate=()=>{
        history.push('/category/create')
    }

    const updateCate=(id)=>{
        history.push('/category/update/'+id)
    }

   return(
    <div>
    <h3>All Category</h3>
    <Button color="info" onClick={createCate} style={{marginBottom:'15px'}}>Add New Category</Button>
    <Table bordered>
        <thead>
            <tr>
                <th>Id</th>
                <th>Category Name</th>
                <th></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            {
                category.map(cate=>
                    <tr>
                        <th>{cate.id}</th>
                        <td>{cate.name}</td> 
                        <td><Button color="success" onClick={()=>updateCate(cate.id)}>Update</Button>{' '}</td>
                        <td><Button color="danger" onClick={()=>deleteCate(cate.id)}>Delete</Button>{' '}</td>
                    </tr>    
                )
            }
        </tbody>
    </Table>
</div>
    )
}


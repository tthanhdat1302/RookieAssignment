import Axios from 'axios';
import React,{useState,useEffect} from 'react'
import { useHistory } from 'react-router-dom'
import { Table,Button } from 'reactstrap';

export default function ProductIndex() {
    const [product,setProduct]=useState([])
    const history=useHistory();

    useEffect(()=>{
        Axios.get("https://localhost:5001/api/product").then(res=>{
            setProduct(res.data)
        })
    },[])

    const deleteProduct=(id)=>{
        Axios.delete("https://localhost:5001/api/product/"+id).then(()=>{
            setProduct(product.filter(x=>x.id!=id))
        })
    }
    
    const createProduct=()=>{
        history.push('/product/create')
    }

    const updateProduct=(id)=>{
        history.push('/product/update/'+id)
    }

   return(
    <div>
    <h3>All Product</h3>
    <Button color="info" onClick={createProduct} style={{marginBottom:'15px'}}>Add New Product</Button>
    <Table bordered className="productTable">
        <thead>
            <tr>
                <th>Id</th>
                <th>Product Name</th>
                <th>Price</th>
                <th>Image</th>
                <th>Rating</th>
                <th>Description</th>
                <th>categoryId</th>
                <th></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            {
                product.map(product=>
                    <tr>
                        <th>{product.id}</th>
                        <td>{product.name}</td> 
                        <td>{product.price}</td> 
                        <td>{product.image}</td>
                        <td>{product.ratingAVG}</td>  
                        <td>{product.description}</td> 
                        <td>{product.categoryId}</td>  
                        <td><Button color="success" onClick={()=>updateProduct(product.id)}>Update</Button>{' '}</td>
                        <td><Button color="danger" onClick={()=>deleteProduct(product.id)}>Delete</Button>{' '}</td>
                    </tr>    
                )
            }
        </tbody>
    </Table>
</div>
    )
}


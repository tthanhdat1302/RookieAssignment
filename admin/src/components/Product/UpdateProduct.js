import React,{useState} from 'react'
import { Button, Form } from 'reactstrap';
import Axios from 'axios'
import { BrowserRouter as Router,useHistory, useParams} from 'react-router-dom'

export default function UpdateProduct() {
    const imgDefault='/img/noImage.jpg'
    let {id}=useParams();

    const [product,setProduct]=useState({
        Name:'',
        Price:0,
        Description:'',
        Image:'',
        ImageFile:null,
        RatingAVG:0,
        CategoryId:2,
        ImageSrc:imgDefault
    });

    const history=useHistory();
    
    const handleChange=e=>{
        const {name,value}=e.target;
        setProduct({...product,[name]:value});
    }

    const showPreview=e=>{
        if(e.target.files && e.target.files[0]){
            let imageFile=e.target.files[0];
            const reader=new FileReader();
            reader.onload=x=>{
                setProduct({
                    ...product,
                    ImageFile:imageFile,
                    ImageSrc:x.target.result
                })
            };
            reader.readAsDataURL(imageFile)
        }
        else{
            setProduct({
                ...product,
                ImageFile:null,
                ImageSrc:imgDefault
            })
        }
    }

    const handleFormSubmit=(e)=>{
        e.preventDefault();
        const formData=new FormData()
        formData.append('Name',product.Name)
        formData.append('Price',parseFloat(product.Price))
        formData.append('Description',product.Description)
        formData.append('Image',product.Image)
        formData.append('ImageFile',product.ImageFile)
        formData.append('RatingAVG',product.RatingAVG)
        formData.append('CategoryId',parseInt(product.CategoryId))
        Axios.put("https://localhost:5001/api/product/"+id,formData
        ).then(()=>{
            history.push('/product')
        })   
    }

    return (
        <div style={{padding:'0 150px 0 150px'}}>
            <h3 style={{margin:'10px 0 10px 0'}}>Edit Product</h3>
            <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
                <div className="form-group">
                    <label>Product Name: </label>
                    <input type="text" className="form-control" name="Name" onChange={handleChange}/>
                </div>             
                <div className="form-group">
                    <label>Price: </label>
                    <input type="text" className="form-control" name="Price" onChange={handleChange}/>
                </div>
                <div className="form-group">
                    <label>Description: </label>
                    <input type="text" className="form-control" name="Description" onChange={handleChange}/>
                </div>
                <div className="form-group">
                    <label>Image: </label><br/>
                    <input type="file" accept="image/*" onChange={showPreview} className="form-control-file"/>
                    <img src={product.ImageSrc} style={{height:'500px',border:'1px solid black'}}></img>
                </div>
                <div className="form-group">
                    <label>Category: </label>
                    <input type="text" className="form-control" name="CategoryId" onChange={handleChange}/>
                </div>
                <div className="form-group">
                    <Button color="success" type="submit" style={{marginRight:'20px'}}>Create Product</Button>
                    <Button color="success" onClick={()=>history.push('/product')}>Cancel</Button>
                </div>
            </form>
        </div>
    )
}


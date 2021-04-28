import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  NavbarText,
} from "reactstrap";

import ProductIndex from "./components/Product/ProductIndex";
import CategoryIndex from "./components/Category/CategoryIndex";
import CreateProduct from "./components/Product/CreateProduct";
import UpdateProduct from "./components/Product/UpdateProduct";
import CreateCategory from "./components/Category/CreateCategory";
import UpdateCategory from "./components/Category/UpdateCategory";
import UserIndex from "./components/User/UserIndex";

import Login from './components/Login/Login'
import LoginCallback from './components/Login/Login-Callback'
import Logout from './components/Logout/Logout'
import LogoutCallback from './components/Logout/Logout-Callback'

import Oidc from 'oidc-client'
import axios from 'axios'
import {useState} from 'react'
function App () {
  var config={
    userStore:new Oidc.WebStorageStateStore({store:window.localStorage}),
    authority:`${process.env.REACT_APP_API_URL}`,
    client_id :"react",
    redirect_uri :`${process.env.REACT_APP_ADMIN_URL}/signin-oidc`,
    post_logout_redirect_uri: `${process.env.REACT_APP_ADMIN_URL}/signout-oidc`,
    response_type :"id_token token",
    scope :"openid profile rookie.api",
  }

  var userManager=new Oidc.UserManager(config);
  userManager.getUser().then(user=>{
    if(user){
      localStorage.setItem("role",user.profile.role)
      axios.defaults.headers.common["Authorization"]="Bearer "+user.access_token
    }
  })
  if(localStorage.getItem("role")!="Admin")
  {
    return(
      <Router>
        <Route exact path="/"><Login userManager={userManager}/></Route>
        <Route exact path="/signin-oidc" ><LoginCallback></LoginCallback></Route>
      </Router>
    )
  }
    return (
      <Router>
      <div>
        <Navbar color="light" light expand="md">
          <Collapse navbar>
            <Nav className="mr-auto" navbar>
              <NavItem>
                <NavLink href="/product">Product</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="/category">Category</NavLink>
              </NavItem>
              <NavItem>
                <NavLink href="/user">User</NavLink>
              </NavItem>            
              <NavItem>
                <NavLink href="/logout">Logout</NavLink>
              </NavItem>            
            </Nav>
          </Collapse>
        </Navbar>
        <Switch>
        <Route exact path="/" component={ProductIndex} />
          <Route exact path="/product" component={ProductIndex} />
          <Route path="/product/create" component={CreateProduct} />
          <Route path="/product/update/:id" component={UpdateProduct} />
  
          <Route exact path="/category" component={CategoryIndex} />
          <Route path="/category/create" component={CreateCategory} />
          <Route path="/category/update/:id" component={UpdateCategory} />
  
          <Route exact path="/user" component={UserIndex}></Route>
  
          {/* <Route exact path="/"><Login userManager={userManager}/></Route> */}
          <Route exact path="/signin-oidc" ><LoginCallback></LoginCallback></Route>
  
          <Route exact path="/logout"><Logout userManager={userManager}/></Route>
          <Route exact path="/signout-oidc" ><LogoutCallback></LogoutCallback></Route>
        </Switch>
      </div>
    </Router>
    ); 
}

export default App;

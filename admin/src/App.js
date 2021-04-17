
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
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
    NavbarText
  } from 'reactstrap';

import ProductIndex from './components/Product/ProductIndex';
import CategoryIndex from './components/Category/CategoryIndex';
import CreateProduct from './components/Product/CreateProduct'
import UpdateProduct from './components/Product/UpdateProduct'
import CreateCategory from './components/Category/CreateCategory'
import UpdateCategory from './components/Category/UpdateCategory'
import UserIndex from './components/User/UserIndex'


function App() {
  return (
    <Router>
                <div>
                    <Navbar color="light" light expand="md">
                        <Collapse navbar>
                        <Nav className="mr-auto" navbar>
                            <NavItem>
                                <NavLink href="/">Home</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink href="/product">Product</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink href="/category">Category</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink href="/user">User</NavLink>
                            </NavItem>
                        </Nav>
                        </Collapse>
                    </Navbar>
                    <Switch>
                        <Route exact path='/product' component={ ProductIndex } />
                        <Route path="/product/create" component={CreateProduct}/>
                        <Route path="/product/update/:id" component={UpdateProduct}/>
                        
                        <Route exact path="/category" component={CategoryIndex}/>
                        <Route path="/category/create" component={CreateCategory}/>
                        <Route path="/category/update/:id" component={UpdateCategory}/>

                        <Route exact path='/user' component={UserIndex}></Route>
                    </Switch>
                </div>        
            </Router>
  );
}

export default App;

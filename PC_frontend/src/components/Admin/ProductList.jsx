import React, { useState, useEffect, useContext } from 'react';
import { Modal, Button, Table, Form } from 'react-bootstrap';
import AuthContext from '../../AuthContext';

const ProductList = () => {
    const [products, setProducts] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState({});
    const { token } = useContext(AuthContext);

    // Fetch products
    const fetchProducts = async () => {
        try {
            const response = await fetch('https://localhost:5888/api/EshopProducts', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            setProducts(data);
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    // Handle product selection for editing
    const handleEdit = (product) => {
        setSelectedProduct(product);
        setShowModal(true);
    };

    // Handle product update
    const handleUpdate = async () => {
        try {
            const response = await fetch(`https://localhost:5888/api/EshopProducts/${selectedProduct.productId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify(selectedProduct),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            setShowModal(false);
            fetchProducts(); // Refresh list
        } catch (error) {
            console.error('Error updating product:', error);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, []);

    return (
        <div className="container">
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Product Name</th>
                        <th>Description</th>
                        <th>Price</th>
                        <th>Available Stock</th>
                        <th>Deleted</th>
                        <th>Edit</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product => (
                        <tr key={product.productId}>
                            <td>{product.productName}</td>
                            <td>{product.description}</td>
                            <td>{product.price}</td>
                            <td>{product.availableStock}</td>
                            <td>{product.deleted ? 'Yes' : 'No'}</td>
                            <td>
                                <Button variant="primary" onClick={() => handleEdit(product)}>Edit</Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Modal show={showModal} onHide={() => setShowModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit Product</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Product Name</Form.Label>
                            <Form.Control 
                                type="text" 
                                value={selectedProduct.productName}
                                onChange={(e) => setSelectedProduct({ ...selectedProduct, productName: e.target.value })}
                            />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Description</Form.Label>
                            <Form.Control 
                                type="text" 
                                value={selectedProduct.description}
                                onChange={(e) => setSelectedProduct({ ...selectedProduct, description: e.target.value })}
                            />
                        </Form.Group>
			                        <Form.Group>
                            <Form.Label>Price</Form.Label>
                            <Form.Control 
                                type="text" 
                                value={selectedProduct.price}
                                onChange={(e) => setSelectedProduct({ ...selectedProduct, price: e.target.value })}
                            />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>availableStock</Form.Label>
                            <Form.Control 
                                type="text" 
                                value={selectedProduct.availableStock}
                                onChange={(e) => setSelectedProduct({ ...selectedProduct, availableStock: e.target.value })}
                            />
                        </Form.Group>


                        <Form.Group>
    <Form.Label>Visible</Form.Label>
    <Form.Select
        value={selectedProduct.deleted ? 'Yes' : 'No'}
        onChange={(e) => setSelectedProduct({ ...selectedProduct, deleted: e.target.value === 'Yes' })}
    >
        <option value="No">No</option>
        <option value="Yes">Yes</option>
    </Form.Select>
</Form.Group>

                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowModal(false)}>Close</Button>
                    <Button variant="primary" onClick={handleUpdate}>Save Changes</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default ProductList;
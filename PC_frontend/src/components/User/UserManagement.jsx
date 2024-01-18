import React, { useState, useEffect, useContext } from 'react';
import { Table, Form } from 'react-bootstrap';
import AuthContext from '../../AuthContext'; // Adjust path as needed

const UserManagement = () => {
    const [users, setUsers] = useState([]);
    const { token } = useContext(AuthContext);

    useEffect(() => {
        const fetchUsers = async () => {
            const response = await fetch('https://localhost:5888/api/auth/getusers', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            });
            if (response.ok) {
                const data = await response.json();
                setUsers(data);
            } else {
                // Handle errors
            }
        };

        fetchUsers();
    }, [token]);

    const handleRoleChange = async (userId, newRoleId) => {
        try {
            const response = await fetch(`https://localhost:5888/api/Auth/ChangeRole/${userId}/${newRoleId}`, {
                method: 'PATCH',
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            // Refresh list after successful role change
            // You may choose to directly update the state or re-fetch users
        } catch (error) {
            console.error('Error changing role:', error);
        }
    };

    return (
        <div className="container">
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>Role</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map((user) => (
                        <tr key={user.userId}>
                            <td>{user.userName}</td>
                            <td>
                                <Form.Select
                                    onChange={(e) => handleRoleChange(user.userId, e.target.value)}
                                    defaultValue={user.roleId}
                                >
                                    <option value="1">Member</option>
                                    <option value="2">Admin</option>
                                    <option value="3">Quality Checker</option>
                                </Form.Select>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
};

export default UserManagement;
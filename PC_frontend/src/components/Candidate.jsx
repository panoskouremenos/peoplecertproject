import React, { useState } from 'react';
import { Form } from 'react-bootstrap';


const Candidate = ({ candidate, onDelete, onEdit }) => {
  const [editing, setEditing] = useState(false);
  const [editedCandidate, setEditedCandidate] = useState({ ...candidate });

  const handleEditClick = () => {
    setEditing(true);
  }
  const handleSaveClick = () => {
    onEdit(candidate.id, editedCandidate);
    setEditing(false);
  };
  const handleCancelClick = () => {
    setEditing(false);
  };
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditedCandidate((prevCandidate) => ({
      ...prevCandidate,
      [name]: value,
    }));
  };
  

  const handleKeyPress = (e) => {
    if (e.key === 'Enter') {
      handleSaveClick();
    }
  };
  
  return (
    <div className="card m-2 text-center">
      <div className="card-body">
        {editing ? (
    <div className="d-flex flex-column gap-3" >
            <h3 className="card-title">Edit Candidate</h3>
            <Form.Group className="mb-3">
  <Form.Label>First Name:</Form.Label>
  <Form.Control
    type="text"
    name="Name"
    value={editedCandidate.Name}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Middle Name:</Form.Label>
  <Form.Control
    type="text"
    name="MiddleName"
    value={editedCandidate.MiddleName}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Last Name:</Form.Label>
  <Form.Control
    type="text"
    name="LastName"
    value={editedCandidate.LastName}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Gender:</Form.Label>
  <Form.Control
    type="text"
    name="Gender"
    value={editedCandidate.Gender}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Native Language:</Form.Label>
  <Form.Control
    type="text"
    name="NativeLanguage"
    value={editedCandidate.NativeLanguage}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Birth Date:</Form.Label>
  <Form.Control
    type="date"
    name="BirthDate"
    value={editedCandidate.BirthDate}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Photo ID Type:</Form.Label>
  <Form.Control
    type="text"
    name="PhotoIDType"
    value={editedCandidate.PhotoIDType}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Photo ID Number:</Form.Label>
  <Form.Control
    type="text"
    name="PhotoIDNumber"
    value={editedCandidate.PhotoIDNumber}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Photo ID Issue Date:</Form.Label>
  <Form.Control
    type="date"
    name="PhotoIDIssueDate"
    value={editedCandidate.PhotoIDIssueDate}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Email:</Form.Label>
  <Form.Control
    type="email"
    name="Email"
    value={editedCandidate.Email}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Address:</Form.Label>
  <Form.Control
    type="text"
    name="Address"
    value={editedCandidate.Address}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Address Line 2:</Form.Label>
  <Form.Control
    type="text"
    name="Address2"
    value={editedCandidate.Address2}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Country:</Form.Label>
  <Form.Control
    type="text"
    name="Country"
    value={editedCandidate.Country}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>State:</Form.Label>
  <Form.Control
    type="text"
    name="State"
    value={editedCandidate.State}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Town:</Form.Label>
  <Form.Control
    type="text"
    name="Town"
    value={editedCandidate.Town}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Postal Code:</Form.Label>
  <Form.Control
    type="text"
    name="PostalCode"
    value={editedCandidate.PostalCode}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Landline Number:</Form.Label>
  <Form.Control
    type="text"
    name="LandlineNumber"
    value={editedCandidate.LandlineNumber}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
<Form.Group className="mb-3">
  <Form.Label>Mobile Number:</Form.Label>
  <Form.Control
    type="text"
    name="MobileNumber"
    value={editedCandidate.MobileNumber}
    onChange={handleInputChange}
    onKeyDown={handleKeyPress}
  />
</Form.Group>
            <div>
            <button className="btn btn-success m-1" onClick={handleSaveClick}>Save</button>
            <button className="btn btn-secondary m-1" onClick={handleCancelClick}>Cancel</button>
            </div>
          </div>
        ) : (
          <>
        <h2>{candidate.Name} {candidate.MiddleName} {candidate.LastName}</h2>
        <p>Gender: <b>{candidate.Gender}</b></p>
        <p>Native Language: <b>{candidate.NativeLanguage}</b></p>
        <p>Birth Date: <b>{candidate.BirthDate}</b></p>
        <p>Photo ID Type <b>{candidate.PhotoIDType}</b></p>
        <p>Photo ID Number : <b>{candidate.PhotoIDNumber}</b></p>
        <p>Photo ID Issue Date : <b>{candidate.PhotoIDIssueDate}</b></p>
        <p>Email: <b>{candidate.Email}</b></p>
        <p>Address : <b>{candidate.Address} {candidate.Address2}</b></p>
        <p>Country : <b>{candidate.Country}</b></p>
        <p>State : <b>{candidate.State}</b></p>
        <p>Town : <b>{candidate.Town}</b></p>
        <p>Postal Code : <b>{candidate.PostalCode}</b></p>
        <p>Landline Number : <b>{candidate.LandlineNumber}</b></p>
        <p>Mobile Number : <b>{candidate.MobileNumber}</b></p>
          <div>
            <button className="btn btn-primary m-1" onClick={handleEditClick}>Edit</button>
            <button className="btn btn-danger m-1" onClick={() => onDelete(candidate.id)}>Delete</button>
          </div>
          </>
        )}
      </div>
    </div>
  );
};

export default Candidate;
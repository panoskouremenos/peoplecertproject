import React, { useState, useEffect, useRef, useContext } from "react";
import AuthContext from "../../AuthContext";
import AlertContext from "../../AlertContext";

const UserControlPanel = () => {
  const [candidate, setCandidate] = useState(null);
  const [loading, setLoading] = useState(true);
  const [editing, setEditing] = useState(false);
  const { token } = useContext(AuthContext);
  const { alerts , setAlerts } = useContext(AlertContext)

  const generateUserPanel = (userData) => {
    setCandidate(userData);
  };

  const fetchUserPersonalData = async (token) => {
    try {
      setLoading(true);
      const response = await fetch("http://localhost:3001/api/user/data", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: token,
        },
      });

      if (response.ok) {
        const userData = await response.json();
        if (token) {
          generateUserPanel(userData);
        }
      }
    } catch (error) {
      console.error("Error fetching user data:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setCandidate((prevCandidate) => ({
      ...prevCandidate,
      [name]: value,
    }));
  };
  
  const handleCancelEdit = () => {
    setEditing(false);
    fetchUserPersonalData(token);
  }

  const handleCandidateValid = () => {
    console.log(candidate);
    handleUpdateCandidate(token)
  }

  const handleUpdateCandidate = async(token) => {
    const newAlerts = [];
    try{
      const response = await fetch("http://localhost:3001/api/user/data/edit", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: token,
        },
        body:JSON.stringify(
          candidate
        )
      });
      if(response.ok){
        newAlerts.push({
          variant: "success",
          message:
          "Your information has been updated.",
        }); 
        handleCancelEdit();
      }else{
        console.log(response);
      }
    }catch(error){
      console.log(error);
    }finally{
      setAlerts(newAlerts);
    }
  }

  useEffect(() => {
    if (token) {
      fetchUserPersonalData(token);
    }
  }, [token]);
  return (
    <div className="container p-5" id="userPanelContainer">
      <div className="row d-flex justify-content-center">
        {loading ? (
          <h1>Loading...</h1>
        ) : (
          <>
            {candidate.isCandidate === true ? (
              <h2 className="text-success">You are a Candidate.</h2>
            ) : (
              <h2 className="text-danger">You aren't a Candidate.</h2>
            )}
            {editing === true ? (
              <>
                <div className="col-md-6 p-2 d-flex flex-column ">
<label>First Name:</label>
<input
  type="text"
  className="form-control mb-2"
  name="FirstName"
  value={candidate.FirstName}
  onChange={handleInputChange}
/>

<label>Middle Name:</label>
<input
  type="text"
  className="form-control mb-2"
  name="MiddleName"
  value={candidate.MiddleName}
  onChange={handleInputChange}
  
/>

<label>Last Name:</label>
<input
  type="text"
  className="form-control mb-2"
  name="LastName"
  value={candidate.LastName}
  onChange={handleInputChange}
  
/>

<label>Gender:</label>
<input
  type="text"
  className="form-control mb-2"
  name="Gender"
  value={candidate.Gender}
  onChange={handleInputChange}
/>

<label>Native Language:</label>
<input
  type="text"
  className="form-control mb-2"
  name="NativeLanguage"
  value={candidate.NativeLanguage}
  
  onChange={handleInputChange}
/>

<label>Birth Date:</label>
<input
  type="date"
  className="form-control mb-2"
  name="BirthDate"
  value={candidate.BirthDate}
  onChange={handleInputChange}
  
/>

<label>Photo ID Type:</label>
<input
  type="text"
  className="form-control mb-2"
  name="PhotoIDType"
  value={candidate.PhotoIDType}
  onChange={handleInputChange}
  
/>

<label>Photo ID Number:</label>
<input
  type="text"
  className="form-control mb-2"
  name="PhotoIDNumber"
  value={candidate.PhotoIDNumber}
  onChange={handleInputChange}
  
/>

<label>Photo ID Issue Date:</label>
<input
  type="date"
  className="form-control mb-2"
  name="PhotoIDIssueDate"
  value={candidate.PhotoIDIssueDate}
  onChange={handleInputChange}
  
/>
                </div>
                <div className="col-md-6 p-2 d-flex flex-column ">
                <label>Email:</label>
<input
  type="email"
  className="form-control mb-2"
  name="Email"
  value={candidate.Email}
  onChange={handleInputChange}
  
/>

<label>Address:</label>
<input
  type="text"
  className="form-control mb-2"
  name="Address"
  value={`${candidate.Address} ${candidate.Address2}`}
  onChange={handleInputChange}
  
/>

<label>Country:</label>
<input
  type="text"
  className="form-control mb-2"
  name="Country"
  value={candidate.Country}
  onChange={handleInputChange}
  
/>

<label>State:</label>
<input
  type="text"
  className="form-control mb-2"
  name="State"
  value={candidate.State}
  onChange={handleInputChange}
  
/>

<label>Town:</label>
<input
  type="text"
  className="form-control mb-2"
  name="Town"
  value={candidate.Town}
  onChange={handleInputChange}
/>

<label>Postal Code:</label>
<input
  type="text"
  className="form-control mb-2"
  name="PostalCode"
  value={candidate.PostalCode}
  onChange={handleInputChange}
  
/>

<label>Landline Number:</label>
<input
  type="text"
  className="form-control mb-2"
  name="LandlineNumber"
  value={candidate.LandlineNumber}
  onChange={handleInputChange}
  
/>

<label>Mobile Number:</label>
<input
  type="text"
  className="form-control mb-2"
  name="MobileNumber"
  value={candidate.MobileNumber}
  onChange={handleInputChange}
  
/>
                </div>
              </>
            ) : (
              <>
                <div className="col-md-6 p-2 d-flex flex-column justify-content-center">
                  <p>
                    First Name: <b>{candidate.FirstName}</b>
                  </p>
                  <p>
                    Middle Name: <b>{candidate.MiddleName}</b>
                  </p>
                  <p>
                    Last Name: <b>{candidate.LastName}</b>
                  </p>
                  <p>
                    Gender: <b>{candidate.Gender}</b>
                  </p>
                  <p>
                    Native Language: <b>{candidate.NativeLanguage}</b>
                  </p>
                  <p>
                    Birth Date: <b>{candidate.BirthDate}</b>
                  </p>
                  <p>
                    Photo ID Type <b>{candidate.PhotoIDType}</b>
                  </p>
                  <p>
                    Photo ID Number : <b>{candidate.PhotoIDNumber}</b>
                  </p>
                  <p>
                    Photo ID Issue Date : <b>{candidate.PhotoIDIssueDate}</b>
                  </p>
                </div>
                <div className="col-md-6 p-2 d-flex flex-column justify-content-center">
                  <p>
                    Email: <b>{candidate.Email}</b>
                  </p>
                  <p>
                    Address :{" "}
                    <b>
                      {candidate.Address} {candidate.Address2}
                    </b>
                  </p>
                  <p>
                    Country : <b>{candidate.Country}</b>
                  </p>
                  <p>
                    State : <b>{candidate.State}</b>
                  </p>
                  <p>
                    Town : <b>{candidate.Town}</b>
                  </p>
                  <p>
                    Postal Code : <b>{candidate.PostalCode}</b>
                  </p>
                  <p>
                    Landline Number : <b>{candidate.LandlineNumber}</b>
                  </p>
                  <p>
                    Mobile Number : <b>{candidate.MobileNumber}</b>
                  </p>
                </div>
              </>
            )}
            {editing === true? 
            ( 
            <>
            <button className="btn btn-save" style={{ width: "auto", paddingLeft: "1em", paddingRight: "1em" }} onClick={(e) => handleCandidateValid()}>Save</button>
            <button className="btn btn-danger" style={{ width: "auto", paddingLeft: "1em", paddingRight: "1em" }} onClick={handleCancelEdit}>Cancel Edit</button>
            </> 
            )
            : 
            (
              <button className="btn btn-add" style={{ width: "auto", paddingLeft: "1em", paddingRight: "1em" }} onClick={(e) => setEditing(true)}>Edit</button>
            )
            }
          </>
        )}
      </div>
    </div>
  );
};

export default UserControlPanel;

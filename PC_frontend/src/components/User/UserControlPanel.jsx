import React, { useState, useEffect, useRef, useContext } from "react";
import AuthContext from "../../AuthContext";
import AlertContext from "../../AlertContext";

const UserControlPanel = () => {
  const [candidate, setCandidate] = useState({
    firstName: "",
    middleName: "",
    lastName: "",
    gender: "",
    nativeLanguage: "",
    birthDate: "",
    email: "",
    landlineNumber: "",
    mobileNumber: "",
    addresses: [{
      address: "",
      addressLine2: "",
      countryOfResidence: "",
      stateTerritoryProvince: "",
      townCity: "",
      postalCode: ""
    }],
    photoIDs: [{
      photoIdtype: 0,
      photoIdnumber: "",
      photoIdissueDate: ""
    }]
  });
  const [loading, setLoading] = useState(true);
  const [editing, setEditing] = useState(false);
  const { token } = useContext(AuthContext);
  const { alerts , setAlerts } = useContext(AlertContext)

  const generateUserPanel = (userData) => {
    if (userData.birthDate) {
      userData.birthDate = formatDateForInput(userData.birthDate);
    }
    if (userData.candidatePhotoIds && userData.candidatePhotoIds.length > 0) {
      userData.candidatePhotoIds[0].photoIdissueDate = formatDateForInput(userData.candidatePhotoIds[0].photoIdissueDate);
    }
    setCandidate(userData);
  };

  const formatDateForInput = (dateString) => {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toISOString().split('T')[0]; 
  };

  const fetchUserPersonalData = async (token) => {
    try {
      setLoading(true);
      const response = await fetch("https://localhost:5888/api/Candidates/MyCandidateInfo", {
        
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          'Authorization': `bearer ${token}`,
        },
      });
      if (response.ok) {
        const userData = await response.json();
        console.log("1")
        console.log(userData)
        if (token) {
            if ('message' in userData){
              generateUserPanel({isCandidate: false});
            }else{
              generateUserPanel({...userData, isCandidate: true});
            }

        }
      }
    } catch (error) {
      console.error("Error fetching user data:", error);
    } finally {
      setLoading(false);
    }
  };

  // const handleInputChange = (e) => {
  //   const { name, value } = e.target;
  //   setCandidate((prevCandidate) => ({
  //     ...prevCandidate,
  //     [name]: value,
  //   }));
  // };
  const fetchUpdateCandidate = async(METHOD) => {
    
    const newAlerts = [];
    try{  
      console.log(JSON.stringify(candidate))
      const response = await fetch("https://localhost:5888/api/Candidates", {
        method: METHOD,
        headers: {
          "Content-Type": "application/json",
          'Authorization': `bearer ${token}`,
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

  const handleCancelEdit = () => {
    setEditing(false);
    fetchUserPersonalData(token);
  }
  const handleCandidateValid = () => {
    console.log(candidate);
    handleUpdateCandidate(token)
  }

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    const keys = name.split('.');
  
    
    setCandidate((prevCandidate) => {
      const updatedCandidate = { ...prevCandidate };

      let currentObject = updatedCandidate;
      for (let i = 0; i < keys.length - 1; i++) {
        if (!currentObject[keys[i]]) {
          currentObject[keys[i]] = {};
        }
        currentObject = currentObject[keys[i]];
      }

      currentObject[keys[keys.length - 1]] = value;

      return updatedCandidate;
    });
  };

  const handleUpdateCandidate = async(token) => {
    const newAlerts = [];
    try{
      const response = await fetch('https://localhost:5888/api/Candidates/MyCandidateInfo',{
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          'Authorization': `bearer ${token}`,
        },
      });
      console.log(response.status)
      if(response.ok){
        const data = await response.json();
        // Check if data has a property 'message'
        if ('message' in data && typeof data.message === 'string'){
          console.log("ΜΑΛΑΚΑ")
          fetchUpdateCandidate("POST");
        }else{
          console.log("ΚΑΡΙΟΛΗ")
          fetchUpdateCandidate("PUT");
        }
      } else {
       console.log("ΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑΑ")
      }   
    }catch(error){}
      
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
  name="firstName"
  value={candidate.firstName}
  onChange={handleInputChange}
/>

<label>Middle Name:</label>
<input
  type="text"
  className="form-control mb-2"
  name="middleName"
  value={candidate.middleName}
  onChange={handleInputChange}
  
/>

<label>Last Name:</label>
<input
  type="text"
  className="form-control mb-2"
  name="lastName"
  value={candidate.lastName}
  onChange={handleInputChange}
  
/>

<label>Gender:</label>
<input
  type="text"
  className="form-control mb-2"
  name="gender"
  value={candidate.gender}
  onChange={handleInputChange}
/>

<label>Native Language:</label>
<input
  type="text"
  className="form-control mb-2"
  name="nativeLanguage"
  value={candidate.nativeLanguage}
  
  onChange={handleInputChange}
/>

<label>Birth Date:</label>
<input
  type="date"
  className="form-control mb-2"
  name="birthDate"
  value={candidate.birthDate}
  onChange={handleInputChange}
  
/>

<label>Photo ID Type:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidatePhotoIds[0].photoId"
  value={candidate.candidatePhotoIds[0].photoId}
  onChange={handleInputChange}
  
/>

<label>Photo ID Number:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidatePhotoIds[0].photoIdnumber"
  value={candidate.candidatePhotoIds[0].photoIdnumber}
  onChange={handleInputChange}
  
/>

<label>Photo ID Issue Date:</label>
<input
  type="date"
  className="form-control mb-2"
  name="candidatePhotoIds[0].photoIdissueDate"
  value={candidate.candidatePhotoIds[0].photoIdissueDate}
  onChange={handleInputChange}
  
/>
                </div>
                <div className="col-md-6 p-2 d-flex flex-column ">
                <label>Email:</label>
<input
  type="email"
  className="form-control mb-2"
  name="email"
  value={candidate.email}
  onChange={handleInputChange}
  
/>

<label>Address:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidateAddresses[0].address"
  value={`${candidate.candidateAddresses[0].address} ${candidate.candidateAddresses[0].addressLine2}`}
  onChange={handleInputChange}
  
/>

<label>Address2:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidateAddresses[0].addressLine2"
  value={`${candidate.candidateAddresses[0].addressLine2}`}
  onChange={handleInputChange}
  
/>

<label>Country:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidateAddresses[0].countryOfResidence"
  value={candidate.candidateAddresses[0].countryOfResidence}
  onChange={handleInputChange}
  
/>

<label>State:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidateAddresses[0].stateTerritoryProvince"
  value={candidate.candidateAddresses[0].stateTerritoryProvince}
  onChange={handleInputChange}
  
/>

<label>Town:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidateAddresses[0].townCity"
  value={candidate.candidateAddresses[0].townCity}
  onChange={handleInputChange}
/>

<label>Postal Code:</label>
<input
  type="text"
  className="form-control mb-2"
  name="candidateAddresses[0].postalCode"
  value={candidate.candidateAddresses[0].postalCode}
  onChange={handleInputChange}
  
/>

<label>Landline Number:</label>
<input
  type="text"
  className="form-control mb-2"
  name="landlineNumber"
  value={candidate.landlineNumber}
  onChange={handleInputChange}
  
/>

<label>Mobile Number:</label>
<input
  type="text"
  className="form-control mb-2"
  name="mobileNumber"
  value={candidate.mobileNumber}
  onChange={handleInputChange}
  
/>
                </div>
              </>
            ) : (
              <>
                <div className="col-md-6 p-2 d-flex flex-column justify-content-center">
                  {console.log(candidate)}
                  <p>
                    First Name: <b>{candidate.firstName}</b>
                  </p>
                  <p>
                    Middle Name: <b>{candidate.middleName}</b>
                  </p>
                  <p>
                    Last Name: <b>{candidate.lastName}</b>
                  </p>
                  <p>
                    Gender: <b>{candidate.gender}</b>
                  </p>
                  <p>
                    Native Language: <b>{candidate.nativeLanguage}</b>
                  </p>
                  <p>
                    Birth Date: <b>{candidate.birthDate}</b>
                  </p>
                  <p>
              
                    Photo ID Type <b>{candidate.candidatePhotoIds[0].photoId}</b>
                  </p>
                  <p>
                    Photo ID Number : <b>{candidate.candidatePhotoIds[0].photoIdnumber}</b>
                  </p>
                  <p>
                    Photo ID Issue Date : <b>{candidate.candidatePhotoIds[0].photoIdissueDate}</b>
                  </p>
                </div>
                <div className="col-md-6 p-2 d-flex flex-column justify-content-center">
                  <p>
                    Email: <b>{candidate.email}</b>
                  </p>
                  <p>
                    Address :
                    <b>
                        {candidate.candidateAddresses[0].address}
                    </b>
                     Address Number :
                    <b>
                        {candidate.candidateAddresses[0].addressLine2}
                    </b>
                  </p>
                  <p>
                    Country : <b>{candidate.candidateAddresses[0].countryOfResidence}</b>
                  </p>
                  <p>
                    State : <b>{candidate.candidateAddresses[0].stateTerritoryProvince}</b>
                  </p>
                  <p>
                    Town : <b>{candidate.candidateAddresses[0].townCity}</b>
                  </p>
                  <p>
                    Postal Code : <b>{candidate.candidateAddresses[0].postalCode}</b>
                  </p>
                  <p>
                    Landline Number : <b>{candidate.landlineNumber}</b>
                  </p>
                  <p>
                    Mobile Number : <b>{candidate.mobileNumber}</b>
                  </p>
                </div>
              </>
            )}
            {editing === true? 
            ( 
            <>
            <button className="btn btn-save" style={{ width: "auto", paddingLeft: "1em", paddingRight: "1em" }} onClick={handleCandidateValid}>Save</button>
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


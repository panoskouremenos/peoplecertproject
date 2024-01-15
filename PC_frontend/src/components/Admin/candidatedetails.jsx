import React, { useState, useEffect, useContext } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import AuthContext from '../../AuthContext';
import locationData from '../../json/countries+states+cities.json';

const formatDate = (dateString) => {
  if (!dateString) return '';
  const date = new Date(dateString);
  return `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getFullYear().toString().substring(2)}`;
};

const CandidateDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { token } = useContext(AuthContext);
  const [candidate, setCandidate] = useState(null);
  const [editing, setEditing] = useState(false);
  const [loading, setLoading] = useState(false);
  const [selectedCountry, setSelectedCountry] = useState("");
  const [selectedState, setSelectedState] = useState("");
  const [selectedCity, setSelectedCity] = useState("");

  useEffect(() => {

    const fetchCandidate = async () => {
      try {
        setLoading(true);
        const response = await fetch(`https://localhost:5888/api/Candidates/${id}`, {
          method: 'GET',
          headers: {
            "Content-Type": "application/json",
            'Authorization': `Bearer ${token}`,
          },
        });
        if (!response.ok) {
          console.log(response.status)
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setCandidate(data);

        setSelectedCountry(data.candidateAddresses[0]?.countryOfResidence || "");
        setSelectedState(data.candidateAddresses[0]?.stateTerritoryProvince || "");
        setSelectedCity(data.candidateAddresses[0]?.townCity || "");

      } catch (error) {
        console.error('Error fetching candidate:', error);
      } finally {
        setLoading(false);
      }
    };

    if (token) {
      fetchCandidate();
    }


  }, [id, token]);

  const buttonStyles = {
    saveButton: "btn btn-success",
    cancelButton: "btn btn-secondary",
    editButton: "btn btn-primary",
    backButton: "btn btn-info",
    deleteButton: "btn btn-danger"
  };

  //The options could be placed on database but there is no need to overcomplicate.
  const genderOptions = {
    false: "Male",
    true: "Female",
  };

  const legaldocOptions = {
    0: "Passport",
    1: "ID card",
    2: "Driver License",
    3: "Military ID Card",
    4: "Birth Certificate",
    5: "Social Security Card"
  };

  const handleLegalDocChange = (e) => {
    const value = parseInt(e.target.value, 10);
    setCandidate(prevCandidate => {
      const updated = { ...prevCandidate };
      updated.candidatePhotoIds[0].photoIdtype = value;
      return updated;
    });
  };

  const handleCountryChange = (e) => {
    const newCountry = e.target.value;
    setSelectedCountry(newCountry);
    setCandidate(prevCandidate => ({
      ...prevCandidate,
      candidateAddresses: [{
        ...prevCandidate.candidateAddresses[0],
        countryOfResidence: newCountry
      }]
    }));
  };

  const handleStateChange = (e) => {
    const newState = e.target.value;
    setSelectedState(newState);
    setCandidate(prevCandidate => ({
      ...prevCandidate,
      candidateAddresses: [{
        ...prevCandidate.candidateAddresses[0],
        stateTerritoryProvince: newState
      }]
    }));
  };

  const handleCityChange = (e) => {
    const newCity = e.target.value;
    setSelectedCity(newCity);
    setCandidate(prevCandidate => ({
      ...prevCandidate,
      candidateAddresses: [{
        ...prevCandidate.candidateAddresses[0],
        townCity: newCity
      }]
    }));
  };

  const handleGenderChange = (e) => {
    const newGenderValue = e.target.value === "true";
    setCandidate(prevCandidate => ({
      ...prevCandidate,
      gender: newGenderValue
    }));
  };
  const handleBack = () => {
    window.location.reload();
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    const keys = name.split('.');

    setCandidate(prevCandidate => {
      let updated = { ...prevCandidate };

      let currentObject = updated;
      keys.forEach((key, index) => {
        if (index === keys.length - 1) {
          currentObject[key] = value;
        } else {
          if (/\[\d+\]/.test(key)) {
            const [arrayName, arrayIndex] = key.match(/[^\[\]]+/g);
            if (!currentObject[arrayName]) {
              currentObject[arrayName] = [];
            }
            if (!currentObject[arrayName][arrayIndex]) {
              currentObject[arrayName][arrayIndex] = {};
            }
            currentObject = currentObject[arrayName][arrayIndex];
          } else {
            if (!currentObject[key]) {
              currentObject[key] = {};
            }
            currentObject = currentObject[key];
          }
        }
      });

      return updated;
    });
  };
  const handleSave = async () => {
    console.log("Selected country:", selectedCountry);
    console.log("Selected state:", selectedState);
    console.log("Selected city:", selectedCity);

    if (selectedCountry === "" || selectedCountry === "Select a Country" ||
      selectedState === "" || selectedState === "Select a State" ||
      selectedCity === "" || selectedCity === "Select a City") {
      alert("Please select a valid country, state, and city.");
      return;
    }
    try {
      const requestBody = {
        ...candidate,
        Addresses: [
          {
            Address: candidate.candidateAddresses[0].address,
            AddressLine2: candidate.candidateAddresses[0].addressLine2,
            CountryOfResidence: selectedCountry,
            StateTerritoryProvince: selectedState,
            TownCity: selectedCity,
            PostalCode: candidate.candidateAddresses[0].postalCode,
          }
        ],
        PhotoIDs: [
          {
            PhotoIdtype: candidate.candidatePhotoIds[0].photoIdtype,
            PhotoIdnumber: candidate.candidatePhotoIds[0].photoIdnumber,
            PhotoIdissueDate: candidate.candidatePhotoIds[0].photoIdissueDate,
          }
        ],
      };

      const response = await fetch(`https://localhost:5888/api/Candidates/${id}`, {
        method: 'PUT',
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`,
        },
        body: JSON.stringify(requestBody),
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
    } catch (error) {
      console.error('Error updating candidate:', error);
    } finally {
      setEditing(false);
    }
  };

  const handleDelete = async () => {
    try {
      const response = await fetch(`https://localhost:5888/api/Candidates/${id}`, {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${token}`,
        },
      });
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      navigate('/candidates');
    } catch (error) {
      console.error('Error deleting candidate:', error);
    }
  };

  if (loading) {
    return <p>Loading...</p>;
  }

  if (!candidate) {
    return <p>No candidate data available.</p>;
  }

  return (
    <div className="container p-5" id="userPanelContainer">
      {editing ? (
        // Edit Mode
        <div className="row">
          <div className="col-md-6">
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
            <select
              name="gender"
              className="form-control mb-2"
              value={genderOptions[candidate.gender]}
              onChange={handleGenderChange}
            >
              <option value="Male">Male</option>
              <option value="Female">Female</option>
            </select>
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
              value={candidate.birthDate.split('T')[0]}
              onChange={handleInputChange}
            />
            <label>Photo ID Type:</label>
            <select
              name="candidatePhotoIds[0]?.photoIdtype"
              className="form-control mb-2"
              value={candidate.candidatePhotoIds[0]?.photoIdtype || ""}
              onChange={handleLegalDocChange}
            >
              {Object.entries(legaldocOptions).map(([key, value]) => (
                <option key={key} value={key}>
                  {value}
                </option>
              ))}
            </select>
            <label>Photo ID Number:</label>
            <input
              type="text"
              className="form-control mb-2"
              name="candidatePhotoIds[0].photoIdnumber"
              value={candidate.candidatePhotoIds[0]?.photoIdnumber || ""}
              onChange={handleInputChange}
            />
            <label>Photo ID Issue Date:</label>
            <input
              type="date"
              className="form-control mb-2"
              name="candidatePhotoIds[0].photoIdissueDate"
              value={candidate.candidatePhotoIds[0]?.photoIdissueDate.split('T')[0] || ""}
              onChange={handleInputChange}
            />
          </div>
          <div className="col-md-6">
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
              value={candidate.candidateAddresses[0]?.address || ""}
              onChange={handleInputChange}
            />
            <label>Address Line 2:</label>
            <input
              type="text"
              className="form-control mb-2"
              name="candidateAddresses[0].addressLine2"
              value={candidate.candidateAddresses[0]?.addressLine2 || ""}
              onChange={handleInputChange}
            />
            <label>Country:</label>
            <select className="form-control mb-2" value={selectedCountry} onChange={handleCountryChange}>
              <option value="">Select a Country</option>
              {locationData.map((country) => (
                <option key={country.id} value={country.name}>
                  {country.name}
                </option>
              ))}
            </select>
            <label>State/Territory/Province:</label>
            <select className="form-control mb-2" value={selectedState} onChange={handleStateChange}>
              <option value="">Select a State</option>
              {locationData.find(country => country.name === selectedCountry)?.states.map((state) => (
                <option key={state.id} value={state.name}>
                  {state.name}
                </option>
              ))}
            </select>
            <label>Town/City:</label>
            <select className="form-control mb-2" value={selectedCity} onChange={handleCityChange}>
              <option value="">Select a City</option>
              {locationData.find(country => country.name === selectedCountry)?.states.find(state => state.name === selectedState)?.cities.map((city) => (
                <option key={city.id} value={city.name}>
                  {city.name}
                </option>
              ))}
            </select>
            <label>Postal Code:</label>
            <input
              type="text"
              className="form-control mb-2"
              name="candidateAddresses[0].postalCode"
              value={candidate.candidateAddresses[0]?.postalCode || ""}
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
          <div className="text-center mt-3">
            <button className="btn btn-primary me-2" onClick={handleSave}>Save</button>
            <button className="btn btn-secondary" onClick={handleBack}>Cancel</button>
          </div>

        </div>
      ) : (
        // Display Mode
        <div className="row">
          <div className="col-md-6">
            <p>First Name: <b>{candidate.firstName}</b></p>
            <p>Middle Name: <b>{candidate.middleName}</b></p>
            <p>Last Name: <b>{candidate.lastName}</b></p>
            <p>Gender: <b>{genderOptions[candidate.gender]}</b></p>
            <p>Native Language: <b>{candidate.nativeLanguage}</b></p>
            <p>Birth Date: <b>{formatDate(candidate.birthDate)}</b></p>
            <p>Photo ID Type: <b>{legaldocOptions[candidate.candidatePhotoIds[0]?.photoIdtype]}</b></p>
            <p>Photo ID Number: <b>{candidate.candidatePhotoIds[0].photoIdnumber}</b></p>
            <p>Photo ID Issue Date: <b>{formatDate(candidate.candidatePhotoIds[0].photoIdissueDate)}</b></p>
          </div>
          <div className="col-md-6">
            <p>Email: <b>{candidate.email}</b></p>
            <p>Address: <b>{candidate.candidateAddresses[0].address}</b></p>
            <p>Address Line 2: <b>{candidate.candidateAddresses[0].addressLine2}</b></p>
            <p>Country: <b>{candidate.candidateAddresses[0].countryOfResidence}</b></p>
            <p>State/Territory/Province: <b>{candidate.candidateAddresses[0].stateTerritoryProvince}</b></p>
            <p>Town/City: <b>{candidate.candidateAddresses[0].townCity}</b></p>
            <p>Postal Code: <b>{candidate.candidateAddresses[0].postalCode}</b></p>
            <p>Landline Number: <b>{candidate.landlineNumber}</b></p>
            <p>Mobile Number: <b>{candidate.mobileNumber}</b></p>
          </div>
          <div className="col-md-12 text-center mt-3">
            <button className="btn btn-warning me-2" onClick={() => setEditing(true)}>Edit</button>

            <button className="btn btn-secondary ms-2" onClick={() => navigate(-1)}>Back</button>
          </div>
          <div className="col-md-2 text-right mt-2">
            <button className="btn btn-danger" onClick={handleDelete}>Delete User</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default CandidateDetails;
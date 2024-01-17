import React, { useState, useEffect } from "react";

import {
  BrowserRouter as Router,
  Routes,
  Route,
  NavLink,
} from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { Alert } from "react-bootstrap";

//IMPORTING COMPONENTS
//import CandidatesPage from "./components/CandidatesPage";
//import CertificatesPage from "./components/CertificatesPage";
import LoginPage from "./components/LoginPage";
import HomePage from "./components/HomePage";
import EShopPage from "./components/EShopPage";
import Logo from "./assets/logo.svg";
import RegisterPage from "./components/RegisterPage";
import ExamPage from "./components/ExamPage";

//CONTEXTS
import AuthContext from "./AuthContext";
import CandidateContext from "./CandidateContext";
import RoleContext from "./RoleContext";
import AlertContext from "./AlertContext";
import ExamContext from './ExamContext';

import UserNavUi from "./components/UserNavUi";
import UserControlPanel from "./components/User/UserControlPanel";
import UserAdminPanel from "./components/User/UserAdminPanel";
import Candidatedetails from "./components/Admin/candidatedetails";
import Candidateslist from "./components/Admin/candidateslist";
import ChangeCreds from "./components/User/ChangeCreds";
import MyCertificates from "./components/User/MyCertificates";
import Certificates from "./components/Admin/certificates";
import MyExams from "./components/User/MyExams";
import AdminCreateCertificate from "./components/Admin/AdminCreateCertificate/AdminCreateCertificate";
//import CreateCert from "./components/Admin/candidatedetails";
import ExamNotification from "./components/ExamNotification";
import Voucher from "./components/Admin/VoucherList";
import PendingExams from "./components/User/PendingExams";

const App = () => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);
  const [isAdmin, setIsAdmin] = useState(null);
  const [isCandidate, setIsCandidate] = useState(null);
  const [examSoon , setIsExamSoon] = useState(null);
  //{ examID : 1 , examTitle : "C# Fundamentals" , timer : new Date()}
  const [alerts , setAlerts] = useState([]);

  const [ timer , setTimer ] = useState("30:00");

  useEffect(() => {
    const intervalId = setInterval(() => {
      const [minutes, seconds] = timer.split(':').map(Number);

      const totalSeconds = minutes * 60 + seconds;

      if (totalSeconds > 0) {
        const newMinutes = Math.floor((totalSeconds - 1) / 60);
        const newSeconds = (totalSeconds - 1) % 60;

        const newTimer = `${String(newMinutes).padStart(2, '0')}:${String(newSeconds).padStart(2, '0')}`;
        setTimer(newTimer);
      } else {
        clearInterval(intervalId);
        console.log("Timer reached 0!");
      }
    }, 1000); 

    return () => clearInterval(intervalId);
  }, [timer]);

  /*
  /auth/user/ExamSoon
  
  { examID : 5 , examDate : new Date() }

  */

  const updateUsername = (newUsername) => {
    setUser(prevUser => ({ ...prevUser, username: newUsername }));
  };

  useEffect(() => {
    const local_token = localStorage.getItem("token");
    if (local_token && token === null) {
      setToken(local_token);
    }
    if (token) {
      fetchUserData(local_token);
    }
  }, [token]);


  const handleLogout = () => {
    setUser(null);
    setIsCandidate(null);
    setIsAdmin(false);
    setToken(null);
    localStorage.removeItem("token");
  };


  const fetchUserData = async (token) => {
    const newAlerts = [];
    try {
      const response = await fetch('https://localhost:5888/api/Auth/GetStatus', {
        method: 'GET',
        headers: {
          'Authorization': `bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        const userData = await response.json();
        console.log(userData);
        setUser({username:userData.userName});
        setIsCandidate(userData.candidateId !== null ? userData.isCandidate : null);
        setIsAdmin(userData.roleId === 2 ? true : false);
      } else {
        handleLogout();
      }
    } catch (error) {
      console.error("Error fetching user data:", error);
    }finally{
      setAlerts(newAlerts); 
    }
  };

  return (
    <Router>
      <AlertContext.Provider value={{ alerts , setAlerts }}>
      <AuthContext.Provider value={{ user, setUser, token, setToken }}>
      <RoleContext.Provider value={{ isAdmin , setIsAdmin }}>
      <CandidateContext.Provider value={{ isCandidate }}>
        <div>
          <nav className="navbar navbar-expand-md bg-white">
            <NavLink to="/" className="navbar-brand ">
              <img src={Logo} title="PeopleCert" />
            </NavLink>
            <div className="container">
              <button
                className="navbar-toggler"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#navbarNav"
                aria-controls="navbarNav"
                aria-expanded="false"
                aria-label="Toggle navigation"
              >
                <span className="navbar-toggler-icon"></span>
              </button>
              <div className="collapse navbar-collapse" id="navbarNav">
                <ul className="navbar-nav">
                  <li className="nav-item">
                    <NavLink to="/" className="nav-link">
                      <FontAwesomeIcon
                        title="Home"
                        icon={faHome}
                        className="mr-1 fs-5"
                      />
                    </NavLink>
                  </li>
                  <li className="nav-item">
                    <NavLink to="/eshop" className="nav-link">
                      EShop
                    </NavLink>
                  </li>

                  {!token ? (
                    <li className="nav-item">
                      <NavLink to="/login" className="nav-link">
                        Login
                      </NavLink>
                    </li>
                  ) : (
                    <>
                    <li className="nav-item">
                    <NavLink to="/user/exams" className="nav-link">
                      Pending Exams
                    </NavLink>
                  </li>
                    <li className="nav-item">
                    <NavLink to="/user/examhistory" className="nav-link">
                      Exam History
                    </NavLink>
                  </li>
                  </>
                  )}
                </ul>
              </div>
            </div>
            <UserNavUi handleLogout={handleLogout} />
          </nav>

          <div className="container mt-4">
          <div id="alerts_box">
            {alerts.map((alert, index) => (
              <Alert
                key={index}
                variant={alert.variant}
                className="text-center"
                onClose={() => setAlerts((prevAlerts) => prevAlerts.filter((_, i) => i !== index))}
                dismissible
              >
                {alert.message}
              </Alert>
            ))}
          </div>
          {examSoon !== null && ( <ExamNotification time={timer} /> ) }
          
            <Routes>
              <Route path="/" element={<HomePage />} />
              <Route path="/register" element={<RegisterPage />} />
              <Route path="/eshop" element={<EShopPage />} />
              <Route path="/exam" element={<ExamPage />} />
              {token !== null ? (
                <>
                  <Route path="/login" element={<HomePage />} />
                  <Route path="/user/cp" element={<UserControlPanel />} />
                  {isAdmin !== false ? (
                    <>
                  <Route path="/user/admin" element={<UserAdminPanel />} />
                  <Route path="admin/certificates" element={<Certificates />} />
                  <Route path="admin/candidates" element={<Candidateslist />} />
                  <Route path="admin/candidates/:id" element={<Candidatedetails />} />
                  <Route path="admin/vouchers" element={<Voucher />} />
                  <Route path="admin/createcert" element={<AdminCreateCertificate />} />
                  </>
                  ) : (
                  <>
                  <Route path="/user/admin" element={<HomePage />} />
                  <Route path="admin/certificates" element={<HomePage />} />
                  <Route path="admin/candidates" element={<HomePage />} />
                  <Route path="admin/candidates/:id" element={<HomePage />} />
                  <Route path="admin/vouchers" element={<HomePage />} />
                   <Route path="admin/createcert" element={<HomePage />} />
                  </>
                  )}
                  {/*<Route path="admin/vouchers" element={<MyExams />} />*/}
                  <Route path="cp/changecreds" element={<ChangeCreds updateUsername={updateUsername} />} />
                  <Route path="user/mycertificates" element={<MyCertificates />} />
                  <Route path="user/examhistory" element={<MyExams />} />
                  <Route path="user/exams" element={<PendingExams />} />


                  
                </>
              ) : (
                <>
                  <Route path="/login" element={<LoginPage />} />
                  <Route path="/user/cp" element={<LoginPage />} />
                </>
              )}
            </Routes>
          </div>
        </div>
        </CandidateContext.Provider>
        </RoleContext.Provider>
      </AuthContext.Provider>
    </AlertContext.Provider>
    </Router>
  );
};

export default App;

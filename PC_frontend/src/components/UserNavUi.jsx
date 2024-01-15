import React, { useContext , useState, useRef } from 'react';
import { Overlay, Popover } from 'react-bootstrap';
import AuthContext from '../AuthContext';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserTie } from '@fortawesome/free-solid-svg-icons';
import { NavLink } from 'react-router-dom';

const UserNavUi = ({ handleLogout }) => {
    const [showPopover, setShowPopover] = useState(false);
    const target = useRef(null);
  
    const handleTogglePopover = () => setShowPopover(!showPopover);
    const handleClickOutside = () => setShowPopover(false);
    const { user } = useContext(AuthContext);
    return (
      <>
        {user ? (
          <div className="user-info">
            <button
              ref={target}
              className="btn btn-white border border-1 border-grey"
              onClick={handleTogglePopover}
            >
        <FontAwesomeIcon icon={faUserTie} alt="User Avatar" className="user-avatar"/>
        <span className="user-name">{user.username}</span>
            </button>
  
            <Overlay
              show={showPopover}
              target={target.current}
              placement="bottom"
              container={target.current}
              containerPadding={20}
            >
              <Popover id="popover-contained">
                <Popover.Header as="h3">User Actions</Popover.Header>
                <Popover.Body>
                  <NavLink to="/user/cp">
                    Control Panel
                  </NavLink>
                  </Popover.Body>
                <Popover.Body>
                <NavLink to="/user/admin" >
                  Admin Panel
                </NavLink>     
                </Popover.Body>
                <Popover.Body>
                { <NavLink to="user/mycertificates" >
                  My Certificates
                </NavLink>      }
                </Popover.Body>
                <Popover.Body>
                  <a href="#" onClick={handleLogout}>Logout</a>
                </Popover.Body>
              </Popover>
            </Overlay>
          </div>
        ) : null}
      </>
    );
  };
  
  export default UserNavUi;
  
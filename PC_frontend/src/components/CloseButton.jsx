import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

const CloseButton = ({ onClick }) => {
  return (
    <button
      type="button"
      className="close-button"
      aria-label="Close"
      onClick={onClick}
    >
      <FontAwesomeIcon icon={faTimes} />
    </button>
  );
};

export default CloseButton;

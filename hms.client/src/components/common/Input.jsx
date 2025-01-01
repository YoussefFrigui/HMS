import React from 'react'

function Input({ label, type = 'text', value, onChange, ...props }) {
  return (
    <div className="input-group">
      {label && <label>{label}</label>}
      <input type={type} value={value} onChange={onChange} {...props} />
    </div>
  )
}

export default Input
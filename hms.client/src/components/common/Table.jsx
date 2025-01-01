import React from 'react'

function Table({ columns, data }) {
  return (
    <table className="common-table">
      <thead>
        <tr>
          {columns.map(col => (
            <th key={col.field}>{col.headerName}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {data.map(row => (
          <tr key={row.id}>
            {columns.map(col => (
              <td key={col.field}>{row[col.field]}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  )
}

export default Table
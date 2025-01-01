import React from 'react'

function MessageList({ messages }) {
  return (
    <ul className="message-list">
      {messages.map(msg => (
        <li key={msg.id} className={`message ${msg.sender === 'me' ? 'sent' : 'received'}`}>
          <span>{msg.content}</span>
          <small>{new Date(msg.timestamp).toLocaleTimeString()}</small>
        </li>
      ))}
    </ul>
  )
}

export default MessageList
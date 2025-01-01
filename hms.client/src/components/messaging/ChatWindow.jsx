import React, { useEffect, useState } from 'react'
import { useMessage } from '@/contexts/MessageContext.jsx'
import MessageList from './MessageList.jsx'
import Input from '@/components/common/Input.jsx'
import Button from '@/components/common/Button.jsx'

function ChatWindow() {
  const { messages, sendMessage, fetchMessages } = useMessage()
  const [newMessage, setNewMessage] = useState('')

  useEffect(() => {
    fetchMessages()
  }, [fetchMessages])

  const handleSend = () => {
    if (newMessage.trim()) {
      sendMessage(newMessage)
      setNewMessage('')
    }
  }

  return (
    <div className="chat-window">
      <MessageList messages={messages} />
      <div className="chat-input">
        <Input
          type="text"
          value={newMessage}
          onChange={e => setNewMessage(e.target.value)}
          placeholder="Type your message..."
        />
        <Button onClick={handleSend}>Send</Button>
      </div>
    </div>
  )
}

export default ChatWindow
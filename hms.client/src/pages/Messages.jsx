import React from 'react'
import ChatWindow from '@/components/messaging/ChatWindow.jsx'
import { MessageProvider } from '@/contexts/MessageContext.jsx'

function Messages() {
  return (
    <MessageProvider>
      <div>
        <h2>Messaging</h2>
        <ChatWindow />
      </div>
    </MessageProvider>
  )
}

export default Messages
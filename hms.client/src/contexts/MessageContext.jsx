import React, { createContext, useContext, useState, useCallback } from 'react'
import { getMessages, postMessage } from '@/services/message.service.js'

const MessageContext = createContext()

export function MessageProvider({ children }) {
  const [messages, setMessages] = useState([])

  const fetchMessages = useCallback(async () => {
    const msgs = await getMessages()
    setMessages(msgs)
  }, [])

  const sendMessage = async (content) => {
    const newMsg = await postMessage({ content })
    setMessages(prev => [...prev, newMsg])
  }

  return (
    <MessageContext.Provider value={{ messages, fetchMessages, sendMessage }}>
      {children}
    </MessageContext.Provider>
  )
}

export function useMessage() {
  return useContext(MessageContext)
}
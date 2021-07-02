import React, { createContext, useContext, useState } from 'react'

const UserContext = createContext({ user: null, setUser: () => { } })

const UserProvider = ({ children }) => {
    const [user, setUser] = useState(JSON.parse(localStorage.getItem('hospital_user')))

    return <UserContext.Provider value={{ user, setUser }}>{children}</UserContext.Provider>
}

const useUserContext = () => {
    const context = useContext(UserContext)
    if (context === undefined)
        throw new Error('useUserContext must be used withn UserProvider')
    return context
}

export { UserProvider, useUserContext }
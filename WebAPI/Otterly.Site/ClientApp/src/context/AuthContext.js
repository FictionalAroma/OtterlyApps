import React, { useState, useEffect, useContext } from "react";

export const AuthContext = React.createContext();
export const useAuth = () => useContext(AuthContext);
export const AuthProvider = ({
    children
}) => {
    const [isAuthenticated, setIsAuthenticated] = useState();
    const [user, setUser] = useState();
    const [isLoading, setIsLoading] = useState(false);

    const getUserSignedIn = async () => {
        const response = await fetch('bff/auth/getUserSignedIn');
        const json = await response.json();

        setIsAuthenticated(json.isAuthenticated);
        setIsLoading(false);
        if (json.isAuthenticated) setUser(json.claims);
    }

    useEffect(() => {
        getUserSignedIn();
    }, []);

    const login = () => {
        window.location.href = 'bff/auth/login';
    }

    const logout = () => {
        window.location.href = 'bff/auth/logout';
    }

    return (
        <AuthContext.Provider
            value={{
                isAuthenticated,
                user,
                isLoading,
                login,
                logout
            }}
        >
            {children}
        </AuthContext.Provider>
    );
};
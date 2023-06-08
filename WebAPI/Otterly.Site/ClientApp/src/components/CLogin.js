import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';


export const CLogin = () => {
    const { login } = useAuth();

    return (
        <div>
        {login()}
        </div>
    )
}


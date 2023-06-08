import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';


export const CLogout = () => {
    const { logout } = useAuth();

    return (
        <div>
        {logout()}
        </div>
    )
}

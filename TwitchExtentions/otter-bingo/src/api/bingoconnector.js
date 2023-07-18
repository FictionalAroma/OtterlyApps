import axios from 'axios'

/**
 * Helper class for authentication against an EBS service. Allows the storage of a token to be accessed across componenents. 
 * This is not meant to be a source of truth. Use only for presentational purposes. 
 */
export default class BingoConnector{

    constructor(apiBaseURL){
        this.apiBaseURL = apiBaseURL;
    }

    validateToken(token)
    {
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };
        axios.get(`${this.apiBaseURL}/twitch/VerifyToken`,config)
        .then((result) => {
            this.cachedToken = token;
        }).catch((err) => {
            
        });
    }


}

import axios from 'axios'

axios.defaults.headers.post['Content-Type'] = 'application/json';
export default class BingoConnector{


    constructor(apiBaseURL){
        this.apiBaseURL = apiBaseURL;
    }
    user=() =>{return this.cachedUser};
    validateToken(token, callback)
    {
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };
        axios.get(`${this.apiBaseURL}/twitch/VerifyToken`,config)
        .then((result) => {
            this.cachedToken = token;
            this.cachedUser = result.data;
            callback(this.cachedUser);
        }).catch((err) => {
            
        });
    }

    sendRequest(url, obj, method, callback)
    {
        const config = {
            headers: { Authorization: `Bearer ${this.cachedToken}` },
            data: JSON.stringify(obj),
            method: method
        };
        axios.request(url, config)
        .then((result) => {
            callback(result.data);
        }).catch((err) => {
            console.log(err);
        });
    }

    getSessionAndTicket(dataCallback)
    {
        const data = {
            StreamerTwitchID: this.cachedUser.broadcasterID,
            PlayerTwitchID: this.cachedUser.userID,
        }
        this.sendRequest(`${this.apiBaseURL}/bingo/player/GetSessionAndTicket`, data, "post", dataCallback)
    }


}

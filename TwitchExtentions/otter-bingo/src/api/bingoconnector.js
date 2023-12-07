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
            if(callback != null) {
                callback(result.data);
            }
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

    markCellSelected(cellIndex, sessionId, twitchUser)
    {
        const data = {
            SessionID: sessionId,
            PlayerTwitchID: this.cachedUser.userID,
            ScreenName: twitchUser.display_name,
            ItemIndex: cellIndex
        }
        this.sendRequest(`${this.apiBaseURL}/bingo/player/markItem`, data, "post")
    }

    createTicket(dataCallback)
    {
        const data = {
            StreamerTwitchID: this.cachedUser.broadcasterID,
            PlayerTwitchID: this.cachedUser.userID,
        }
        this.sendRequest(`${this.apiBaseURL}/bingo/player/createTicket`, data, "post", dataCallback)
    }

    verifyItem(itemIndex, sessionId, onOff)
    {
        const data = {
            SessionID: sessionId,
            ItemIndex: itemIndex,
            State: onOff,
        }
        this.sendRequest(`${this.apiBaseURL}/bingo/game/verifyItem`, data, "post")

    }


}

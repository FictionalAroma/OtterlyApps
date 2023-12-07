import axios from 'axios'

axios.defaults.headers.post['Content-Type'] = 'application/json';
export default class TwitchConnector {
    twitchBaseURL = "https://api.twitch.tv";

    setupUser = (twitchAuth, callback) =>
    {
        const config = {
            headers: { Authorization: `Bearer ${twitchAuth.token}` },
        };
        axios.get(`${this.twitchBaseURL}/helix/getUsers`, config)
        .then((result) => 
        {
            if(result.data.length > 0)
            {
                const gotUser = this.userData = result.data[0];
                this.cachedUser = gotUser;
                callback(gotUser);
            }
        });

    };
}
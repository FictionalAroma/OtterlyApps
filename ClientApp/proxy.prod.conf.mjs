export default [
  {
    context: [
        '/bff'
        ],
    target: 'https://ec2-3-249-25-187.eu-west-1.compute.amazonaws.com:7172',
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];

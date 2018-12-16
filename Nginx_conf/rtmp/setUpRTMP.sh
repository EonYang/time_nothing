sudo apt-get update
echo Y |sudo apt-get install build-essential libpcre3 libpcre3-dev libssl-dev

mkdir rtmp

cd rtmp

wget http://nginx.org/download/nginx-1.7.5.tar.gz
wget https://github.com/arut/nginx-rtmp-module/archive/master.zip
echo yes | sudo apt-get install unzip
tar -zxvf nginx-1.7.5.tar.gz
unzip master.zip
cd nginx-1.7.5

./configure --with-http_ssl_module --add-module=../nginx-rtmp-module-master

make
sudo make install

sudo wget https://raw.github.com/JasonGiedymin/nginx-init-ubuntu/master/nginx -O /etc/init.d/nginx
sudo chmod +x /etc/init.d/nginx
sudo update-rc.d nginx defaults

sudo service nginx start
sudo service nginx stop

sudo apt-get install software-properties-common
echo -ne '\n' | sudo add-apt-repository ppa:kirillshkrogalev/ffmpeg-next

sudo apt-get update

echo Y | sudo apt-get install ffmpeg

sudo cp /usr/local/nginx/conf/nginx.conf /usr/local/nginx/conf/nginx.conf.original
sudo vim /usr/local/nginx/conf/nginx.conf

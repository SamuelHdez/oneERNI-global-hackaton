import io
import picamera
from picarx import Picarx
from robot_hat import TTS
import pyttsx3

import time
from flask import Flask, render_template, Response,request
from flask_talisman import Talisman
import requests
tts_robot = TTS()
cameraposy=0
cameraposx=0
temprace=None


app = Flask(__name__)
Talisman(app,content_security_policy=None)
blocked=False
playing=False
cblocked=False
recording=False
@app.route('/')
def index():
    return ok_response()
    
@app.route('/keepalive') # Use this method to check if the robot is connected to internet and reachable.
def keepalive():
    return ok_response()
    
@app.route('/talk', methods=['POST']) # Use this method to send a phrase and make the robot say it 
def talk():
    try:
        texto = request.data.decode('utf-8')
        engine.setProperty('volume', 1)
        engine.say(texto)
        engine.runAndWait()
        return ok_response()

    except Exception as e:
        print("Error"+str(e))
        return nok_response()
        
@app.route('/startrecording', methods=['POST']) # Use this method to start recording robot movements.
def startrecording():
    global recording
    global temprace
    recording=True
    ruta_archivo = '/tmp/temproute'  
    temprace = open(ruta_archivo, 'w')
    return ok_response()
    
@app.route('/endrecording', methods=['POST']) # Use this method to stop recording robot movements.
def endrecording():
    global recording
    global temprace
    recording=False
    if temprace is not None:
        temprace.close()
    return ok_response()
    
@app.route('/followline', methods=['POST']) # Use this method to make the Robot follow a black line in the floor.
def followline():
    front=0
    left=0
    Right=0
    last=""
    while True:
        gm_val_list = px.get_grayscale_data()
        print("gm_val_list:",gm_val_list)
        gm_status = px.get_line_status(gm_val_list)
        print("gm_status:",gm_status)

       
        if gm_status[1] == 1 :
            last="front"
            px.set_motor_speed(0, -60)
            px.set_motor_speed(1, 60) 
            time.sleep(1/1000)
        if gm_status[2] == 1 and gm_status[0] == 0:
            last="left"
            px.set_motor_speed(0, -50)
            px.set_motor_speed(1, 1)
            
            time.sleep(1/1000)
           
        if gm_status[0] == 1 and gm_status[2] == 0:
            last="right"
            px.set_motor_speed(0, -10)
            px.set_motor_speed(1, 50)
            time.sleep(1/1000)
       

        if gm_status[0] == 0 and  gm_status[1] == 0  and gm_status[2] == 0:
             px.set_motor_speed(0, -1)
             px.set_motor_speed(1, 1)
             time.sleep(1/10000)
    return ok_response() 


@app.route('/playlastrecordedrace') # Use this method to replay the last recorded robot movements.
def playlastrecordedrace():
    global blocked 
    
    with open('/tmp/temproute', 'r') as archivo:
        blocked=True
        for linea in archivo:
            print(linea)
            if linea=="front\n":
                px.set_motor_speed(1, 100) # adelante
                px.set_motor_speed(0, -90)
                time.sleep(1/10)
            if linea=="rear\n":
                px.set_motor_speed(1, -100) # atrás
                px.set_motor_speed(0, 90)
                time.sleep(1/10)
            if linea=="left\n":
                px.set_motor_speed(1, -100)
                px.set_motor_speed(0, -95)# izquierda
                
                time.sleep(1/10)
            if linea=="right\n":
                px.set_motor_speed(1, 100)
                px.set_motor_speed(0, 95)# derecha
                
                time.sleep(1/10)
            px.set_motor_speed(0, 0)# derecha
            px.set_motor_speed(1, 0)
            time.sleep(1/16)
        blocked=False
    return ok_response()        

@app.route('/rear', methods=['POST']) # Move robot backwards
def rear():
    global blocked 
    if blocked==False:
        blocked=True
        if recording==True:
            if temprace is not None:
                temprace.write("rear\n")
        px.set_motor_speed(1, -100) # atrás
        px.set_motor_speed(0, 90)
        time.sleep(1/4)
        px.set_motor_speed(0, 0)
        px.set_motor_speed(1, 0)
        blocked=False
    return ok_response()
    
@app.route('/front', methods=['POST']) # Move robot forward
def front():
    global blocked 
    if blocked==False:
        blocked=True
        if recording==True:
            if temprace is not None:
                temprace.write("front\n")
        px.set_motor_speed(1, 100) # adelante
        px.set_motor_speed(0, -90)
        time.sleep(1/4)
        px.set_motor_speed(0, 0)
        px.set_motor_speed(1, 0)
        blocked=False
    return ok_response()
    
@app.route('/left', methods=['POST']) # Turn Robot to left
def left():
    global blocked 
    if blocked==False:
        blocked=True
        if recording==True:
            if temprace is not None:
                temprace.write("left\n")
        px.set_motor_speed(0, -95)# izquierda
        px.set_motor_speed(1, -100)
        time.sleep(1/10)
        px.set_motor_speed(0, 0)
        px.set_motor_speed(1, 0)
        time.sleep(3/16)
        blocked=False
    return ok_response()
    
@app.route('/right', methods=['POST']) # Turn Robot to right
def right():
    global blocked 
    if blocked==False:
        blocked=True
        if recording==True:
            if temprace is not None:
                temprace.write("right\n")
        px.set_motor_speed(0, 95)# derecha
        px.set_motor_speed(1, 100)
        time.sleep(1/10)
        px.set_motor_speed(0, 0)
        px.set_motor_speed(1, 0)
        time.sleep(3/16)
        blocked=False
    return ok_response()
    
@app.route('/cup', methods=['POST']) # Move camera up
def cup():
    global cameraposy 
    global cblocked 
    if cblocked==False and cameraposy<67:
        cblocked=True
        cameraposy=cameraposy+2
        px.set_camera_tilt_angle(cameraposy)
        time.sleep(0.01)
        print (cameraposy)
        cblocked=False
    return ok_response()
    
@app.route('/cdown', methods=['POST']) # Move camera down
def cdown():
    global cameraposy  
    global cblocked 
    if cblocked==False and cameraposy>-18:
        cblocked=True
        cameraposy=cameraposy-2
        px.set_camera_tilt_angle(cameraposy)
        time.sleep(0.01)
        print (cameraposy)
        cblocked=False
    return ok_response()
    
@app.route('/cleft', methods=['POST']) # Move camera left
def cleft():
    global cameraposx 
    global cblocked 
    if cblocked==False:
        cblocked=True
        cameraposx=cameraposx-2
        px.set_cam_pan_angle(cameraposx)
        time.sleep(0.01)
        print (cameraposx)
        cblocked=False
    return ok_response()
    
@app.route('/cright', methods=['POST']) # Move camera right
def cright():
    global cameraposx 
    global cblocked 
    if cblocked==False:
        cblocked=True
        cameraposx=cameraposx+2
        px.set_cam_pan_angle(cameraposx)
        print (cameraposx)
        time.sleep(0.01)
        cblocked=False
    return ok_response()
    
@app.route('/ccenter', methods=['POST']) # Center camera
def ccenter():
    global cameraposx 
    global cameraposy
    global cblocked 
    if cblocked==False:
        cblocked=True
        cameraposx=0
        cameraposy=0
        px.set_cam_pan_angle(cameraposx)
        px.set_camera_tilt_angle(cameraposy)
        print (cameraposx)
        time.sleep(0.01)
        cblocked=False
    return ok_response()

def generate_frames(): # Set up Camera capture & stream settings
    with picamera.PiCamera() as camera:
        camera.resolution = (320, 240)
        camera.framerate = 5

        while True:
            stream = io.BytesIO()
            for _ in camera.capture_continuous(stream, 'jpeg', use_video_port=True):
                stream.seek(0)
                yield (b'--frame\r\n'
                       b'Content-Type: image/jpeg\r\n\r\n' + stream.read() + b'\r\n')
                stream.seek(0)
                stream.truncate()

@app.route('/video_feed')  # Publish camera stream
def video_feed():
    return Response(generate_frames(),
                    mimetype='multipart/x-mixed-replace; boundary=frame')

def ok_response():
    return Response(status=200)

def nok_response():
    return Response(status=500)

if __name__ == '__main__':
    blocked=False
    px = Picarx()
    engine = pyttsx3.init()
    px.set_camera_tilt_angle(0)
    px.set_cam_pan_angle(0)
    app.run(host='0.0.0.0', port=8000, debug=True, ssl_context=('/etc/letsencrypt/live/jdarknessdomains.ddns.net/fullchain.pem','/etc/letsencrypt/live/jdarknessdomains.ddns.net/privkey.pem'))

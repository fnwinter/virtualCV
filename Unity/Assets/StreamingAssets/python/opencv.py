import cv2

capture = cv2.VideoCapture("udp://127.0.0.1:1234")
capture.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
capture.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)

while True:
    ret, frame = capture.read()
    cv2.imshow("VideoFrame", frame)
    if cv2.waitKey(1) == ord('q'): break

capture.release()
cv2.destroyAllWindows()

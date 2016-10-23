import cv2
from chainer import Variable, serializers
from net import FastStyleNet
import numpy as xp
import argparse

model = FastStyleNet()

def _transform(in_image,loaded,m_path):
    serializers.load_npz(m_path, model)

    image = xp.asarray(in_image, dtype=xp.float32).transpose(2, 0, 1)
    image = image.reshape((1,) + image.shape)

    x = Variable(image)
    y = model(x)

    result = y.data
    result = result.transpose(0, 2, 3, 1)
    result = result.reshape((result.shape[1:]))
    result = xp.uint8(result)

    return result

def trans(input,model,out):
    frame = cv2.imread(args.input)
    frame = cv2.resize(frame,(0,0),fx=0.8,fy=0.8)
    frame = cv2.resize( _transform(frame,True,args.model), (0,0), fx=1.25, fy=1.25)
    cv2.imwrite(args.out,frame)

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='fast neural style')
    parser.add_argument('input')
    parser.add_argument('--model', '-m', default='models/kanagawa.model', type=str)
    parser.add_argument('--out', '-o', default='out.jpg', type=str)
    args = parser.parse_args()

    trans(args.input,args.model,args.out);


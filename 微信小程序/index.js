//index.js
//获取应用实例
const app = getApp()

Page({
  data:{
    device_id: '607169499',
    datastreamsImage: 'image',
    datastreamsData: 'direction',
    index:"607169499_1593272113910_image",
    imgSrc: 'https://res.wx.qq.com/wxdoc/dist/assets/img/0.4cb08bb4.jpg',
    timer: "",      //定时器
  },
  
  onLoad: function () {
    var _this = this;
     console.log(this.data.device_id);// 打印设备ID
    console.log(app.data.master_api_key);// 打印API_KEY
    // var index = 1;
    // _this.timer = setInterval(function () {
      
    //   if (index == 1) {
    //     clearInterval(_this.timer)
    //   }
    //   _this.receiveCmd()
    //   console.log('1s执行1次且index+1,等于10停止 index:', index);
    //   index = index + 1;

    // }, 1000);
    wx.request({
      url: 'https://api.heclouds.com/devices/' + this.data.device_id + '/datastreams/' + this.data.datastreamsImage,
      header: {
        'api-key': app.data.master_api_key
      },
      method: 'GET',
      success: function(res){
        _this.setData(
          {
            index: res.data.data.current_value.index
          }
        )
      },
      fail: function(res){
        console.log(res)
      }
    })
    wx.request({
      url: 'https://api.heclouds.com/devices/' + this.data.device_id + '/datapoints',
      header: {
        'api-key': app.data.master_api_key
      },
      method: 'POST',
      data: {
        "datastreams": [{
                "id": "direction",
                "datapoints": [{
                        "value": 0
                }]
            }
        ]
    },
      success: function(res){
        console.log(res)
      },
      fail: function(res){
        console.log(res)
      }
    })
  },
  sendCmd: function(event){
    var _this = this;
    let query = event.currentTarget.dataset['index'];
    wx.request({
      url: 'https://api.heclouds.com/devices/' + this.data.device_id + '/datapoints',
      header: {
        'api-key': app.data.master_api_key
      },
      method: 'POST',
      data: {
        "datastreams": [{
                "id": "direction",
                "datapoints": [{
                        "value": query
                }]
            }
        ]
    },
      success: function(res){
        console.log(res)
      },
      fail: function(res){
        console.log(res)
      }
    })
  },
  receiveCmd: function(){
    var _this = this;
    wx.request({
      url: 'https://api.heclouds.com/devices/' + this.data.device_id + '/datastreams/' + this.data.datastreamsImage,
      header: {
        'api-key': app.data.master_api_key
      },
      method: 'GET',
      success: function(res){
        _this.setData(
          {
            index: res.data.data.current_value.index
          }
        )
      },
      fail: function(res){
        console.log(res)
      }
    })
    console.log(this.data.index)
    wx.request({
      url: 'https://api.heclouds.com/bindata/' + this.data.index,
      header: {
        'api-key': app.data.master_api_key
      },
      method: 'GET',
      responseType: 'arraybuffer',
      success: function(res){
        // console.log(res)
        _this.setData({
          expressData: res.data,
        })
        var base64 = wx.arrayBufferToBase64(res.data);
         console.log('ok');
        _this.setData({
          imgSrc: 'data:image/jpg;base64,' + base64
        })
      },
      fail: function(res){
        console.log(res)
      }
    })
  },
  onUnload: function () {
    clearInterval(this.timer)
    console.log("==onUnload==");
  },

  onHide: function(){
    clearInterval(this.timer)
    console.log("==onHide==");
  },

  onShow: function(){
    var _this = this;
    var index = 1;
    wx.request({
      url: 'https://api.heclouds.com/devices/' + this.data.device_id + '/datapoints',
      header: {
        'api-key': app.data.master_api_key
      },
      method: 'POST',
      data: {
        "datastreams": [{
                "id": "direction",
                "datapoints": [{
                        "value": 0
                }]
            }
        ]
    },
      success: function(res){
        console.log(res)
      },
      fail: function(res){
        console.log(res)
      }
    })

    _this.timer = setInterval(function () {
      _this.receiveCmd()
    }, 200);
  },
})
